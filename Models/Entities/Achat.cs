namespace ExpressVoituresApp.Models.Entities
{
    public class Achat
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule? Vehicule { get; set; }
    }
}
