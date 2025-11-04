using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Entities
{
    public class Annonce
    {
        public int Id { get; set; }

        public required string Titre { get; set; }

        public required string Description { get; set; }

        public string? Photo { get; set; }

        public string? Statut { get; set; }

        public int Prix { get; set; }

        public DateTime DatePublication { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; }
    }
}
