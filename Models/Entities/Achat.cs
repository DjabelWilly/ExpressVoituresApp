using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Entities
{
    public class Achat
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Precision(10, 2)]
        public decimal Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; }
    }
}
