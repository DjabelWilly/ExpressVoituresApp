namespace ExpressVoituresApp.Models
{
    public class Annonce
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Statut { get; set; }

        // FK
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
