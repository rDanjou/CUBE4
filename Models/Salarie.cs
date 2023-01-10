using System.ComponentModel.DataAnnotations;

namespace CUBE.Models
{
    public class Salarie
    {
        public int SalarieId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nom { get; set; } = "";

        [Required]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = "";

        [Required]
        [StringLength(15)]
        [Display(Name = "Téléphone Fixe")]
        public string TelephoneFixe { get; set; } = "";

        [Required]
        [StringLength(15)]
        [Display(Name = "Téléphone Portable")]
        public string TelephonePortable { get; set; } = "";

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "";

        [Required]
        public virtual Service? Service { get; set; }

        [Required]
        public virtual Site? Site { get; set; } 
    }
}
