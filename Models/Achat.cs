namespace ExpressVoituresApp.Models
{
    public class Achat
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
