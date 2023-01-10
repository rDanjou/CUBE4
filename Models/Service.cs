using System.ComponentModel.DataAnnotations;

namespace CUBE.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [StringLength(250)]
        public string Nom { get; set; } = "";
    }
}
