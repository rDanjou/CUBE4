using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUBE.Models;
using CUBE4.Data;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CUBE4.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index(string searchQueryNom = "", string searchQueryPrenom = "")
        {
            List<Salarie> tservices = await _context.Salarie.ToListAsync();
            List<Salarie> services = new List<Salarie>();

            foreach (Salarie s in tservices)
            {
                if ((string.IsNullOrEmpty(searchQueryNom) || s.Nom.ToLower().Contains(searchQueryNom.ToLower())) &&
                    (string.IsNullOrEmpty(searchQueryPrenom) || s.Prenom.ToLower().Contains(searchQueryPrenom.ToLower())))
                { services.Add(s); }
            }

            return View(services);
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Salarie == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salarie
                .FirstOrDefaultAsync(m => m.SalarieId == id);

            var sites = await _context.Site
                .FirstOrDefaultAsync(m => m.SiteId == salarie.Site.SiteId);

            var services = await _context.Service
                .FirstOrDefaultAsync(m => m.ServiceId == salarie.Service.ServiceId);

            ViewBag.Services = services;
            ViewBag.Sites = sites;

            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            var sites = _context.Site.Select(s => new SelectListItem
            {
                Value = s.SiteId.ToString(),
                Text = s.Ville
            }).ToList();

            var services = _context.Service.Select(s => new SelectListItem
            {
                Value = s.ServiceId.ToString(),
                Text = s.Nom
            }).ToList();

            // Add the data to the ViewData object


            ViewBag.Services = new SelectList(_context.Service, "ServiceId", "Nom", services);
            ViewBag.Sites = new SelectList(_context.Site, "SiteId", "Ville", sites);


            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salarie newSalarie)
        {
            Service? service = _context.Service.FirstOrDefault(x => x.ServiceId == newSalarie.Service.ServiceId);
            if (service == null)
            {
                return NotFound();
            }

            Site? site = _context.Site.FirstOrDefault(x => x.SiteId == newSalarie.Site.SiteId);
            if (site == null)
            {
                return NotFound();
            }

            Salarie? salarie = new Salarie()
            {
                Nom = newSalarie.Nom,
                Prenom = newSalarie.Prenom,
                TelephoneFixe = newSalarie.TelephoneFixe,
                TelephonePortable = newSalarie.TelephonePortable,
                Email = newSalarie.Email,
                Service = service,
                Site = site,
            };

            _context.Add(salarie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            // GET: Salaries/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salarie == null)
            {
                return NotFound();
            }


            var site = await _context.Site.FindAsync(id);

            var service = await _context.Service.FindAsync(id);

            var salarie = _context.Salarie
            .Include(e => e.Service)
            .Include(e => e.Site)
            .SingleOrDefault(e => e.SalarieId == id);

            ViewBag.Services = new SelectList(_context.Service, "ServiceId", "Nom", service);
            ViewBag.Sites = new SelectList(_context.Site, "SiteId", "Ville", site);                                                             

            if (salarie == null)
            {
                return NotFound();
            }
            return View(salarie);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalarieId,Nom,Prenom,TelephoneFixe,TelephonePortable,Email")] Salarie salarie)
        {
            if (id != salarie.SalarieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salarie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarieExists(salarie.SalarieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salarie);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Salarie == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salarie
                .FirstOrDefaultAsync(m => m.SalarieId == id);
            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Salarie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Salarie'  is null.");
            }
            var salarie = await _context.Salarie.FindAsync(id);
            if (salarie != null)
            {
                _context.Salarie.Remove(salarie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarieExists(int id)
        {
            return _context.Salarie.Any(e => e.SalarieId == id);
        }
    }
}
