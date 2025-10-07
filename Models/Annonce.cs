using System.ComponentModel.DataAnnotations;

namespace ExpressVoituresApp.Models
{
    public class Annonce
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        public required string Titre { get; set; }

        [Required(ErrorMessage = "La description est requise")]
        public required string Description { get; set; }

        public string? Photo { get; set; }

        [Required(ErrorMessage = "Le statut est requis")]
        public required string Statut { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public required Vehicule Vehicule { get; set; }
    }
}
