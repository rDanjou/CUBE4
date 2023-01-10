using System.ComponentModel.DataAnnotations;

namespace CUBE.Models
{
    public class Site
    {
        public int SiteId { get; set; }

        [Required]
        [StringLength(100)]
        public string Ville { get; set; } = "";

        [Required]
        [StringLength(250)]
        public string Fonction { get; set; } = "";
    }
}
