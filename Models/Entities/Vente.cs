using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Entities
{
    public class Vente
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateDisponibilite { get; set; }

        [Precision(10, 2)]
        public decimal Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public required Vehicule Vehicule { get; set; }
    }
}
