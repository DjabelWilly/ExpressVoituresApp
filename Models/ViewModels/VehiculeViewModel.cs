using System.ComponentModel.DataAnnotations;

namespace ExpressVoituresApp.Models
{
    public class VehiculeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le code VIN est requis")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Le code VIN doit contenir 17 caractères.")]
        [RegularExpression("^[A-Za-z0-9]*$", ErrorMessage = "Le code VIN ne doit contenir que des lettres et des chiffres.")]
        public required string CodeVin { get; set; }

        [Required(ErrorMessage = "La marque est requise")]
        public required string Marque { get; set; }

        [Required(ErrorMessage = "Le modèle est requis")]
        public required string Modele { get; set; }

        public string? Finition { get; set; }

        [Required(ErrorMessage = "L'année est requise")]
        public int Annee { get; set; }

    }
}
