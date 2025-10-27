using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Entities
{
    public class Reparation
    {
        public int Id { get; set; }

        public required string Description { get; set; }

        [Precision(10, 2)]
        public required decimal Cout { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; }
    }
}
