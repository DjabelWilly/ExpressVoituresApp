namespace ExpressVoituresApp.Models.Entities
{
    public class Vehicule
    {
        public int Id { get; set; }

        public required string CodeVin { get; set; }

        public required string Marque { get; set; }

        public required string Modele { get; set; }

        public string? Finition { get; set; }

        public int Annee { get; set; }

        // Relations
        public Achat? Achat { get; set; }
        public Vente? Vente { get; set; }
        public Annonce? Annonce { get; set; }
        public ICollection<Reparation> Reparations { get; set; } = new List<Reparation>();
    }
}
