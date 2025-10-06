namespace ExpressVoituresApp.Models
{
    public class Vente
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateDisponibilite { get; set; }
        public decimal Prix { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
