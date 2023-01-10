using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CUBE.Models;

namespace CUBE4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CUBE.Models.Site> Site { get; set; }
        public DbSet<CUBE.Models.Service> Service { get; set; }
        public DbSet<CUBE.Models.Salarie> Salarie { get; set; }
    }
}