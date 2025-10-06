using System;
using Humanizer.Localisation;

namespace ExpressVoituresApp.Models
{
    public class Vehicule
    {
        public int Id { get; set; }
        public string CodeVin { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public string Finition { get; set; }
        public int Annee { get; set; }

        // Relations
        public Achat Achat { get; set; }
        public Vente Vente { get; set; }
        public Annonce Annonce { get; set; }
        public ICollection<Reparation> Reparations { get; set; }
    }
}
