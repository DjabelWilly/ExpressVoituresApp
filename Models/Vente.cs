using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models
{
    public class Vente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date de vente est requise")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "La date de disponibilité est requise")]
        public DateTime? DateDisponibilite { get; set; }

        [Required(ErrorMessage = "Le prix de vente est requis")]
        [Precision(10, 2)]
        public decimal Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public required Vehicule Vehicule { get; set; }
    }
}
