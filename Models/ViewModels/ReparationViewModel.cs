using System.ComponentModel.DataAnnotations;

namespace ExpressVoituresApp.Models
{
    public class ReparationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La description est requise")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Le coût est requis")]
        [Range(0.01, 1000000, ErrorMessage = "Le prix doit être positif")]
        public decimal Cout { get; set; }

        [Required(ErrorMessage = "Le véhicule est requis")]
        [Display(Name = "Véhicule")]
        public int VehiculeId { get; set; }

    }
}
