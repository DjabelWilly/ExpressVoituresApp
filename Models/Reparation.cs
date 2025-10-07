using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models
{
    public class Reparation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La description est requise")] 
        public required string Description { get; set; }

        [Required(ErrorMessage = "Le coût est requis")]
        [Precision(10, 2)]
        public required decimal Cout { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public required Vehicule Vehicule { get; set; }
    }
}
