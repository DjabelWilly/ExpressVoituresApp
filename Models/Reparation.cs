namespace ExpressVoituresApp.Models
{
    public class Reparation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Cout { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
