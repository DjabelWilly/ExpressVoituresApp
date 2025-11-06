using System.ComponentModel.DataAnnotations;
using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models
{
    public class AnnonceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        public required string Titre { get; set; }

        [Required(ErrorMessage = "La description est requise")]
        public required string Description { get; set; }

        public string? Photo { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? Statut { get; set; }

        public int Prix { get; set; }

        public DateTime DatePublication { get; set; }

        public int VehiculeId { get; set; }

        // Liste pour le menu déroulant
        public IEnumerable<Vehicule>? Vehicules { get; set; }
    }
}
