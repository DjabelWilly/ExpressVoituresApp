using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models
{
    public class Achat
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date est requise")] 
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Le prix d'achat est requis")]
        [Precision(10, 2)]
        public decimal Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public required Vehicule Vehicule { get; set; }
    }
}
