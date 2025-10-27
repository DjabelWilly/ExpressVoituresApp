using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models
{
    public class VehiculeReparationViewModel
    {
        public VehiculeViewModel Vehicule { get; set; } = null!;

        public ReparationViewModel Reparation { get; set; } = null!;
    
    }
    
}


// ViewModel composite, regroupe Reparation et Vehicule