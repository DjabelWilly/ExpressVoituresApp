namespace ExpressVoituresApp.Models.ViewModels
{
    public class VehiculeAchatViewModel
    {
        public VehiculeViewModel Vehicule { get; set; } = null!;
        public AchatViewModel Achat { get; set; } = null!;
        public AnnonceViewModel? Annonce { get; set; }
        public VenteViewModel? Vente { get; set; }
        public VehiculeReparationViewModel? Reparation { get; set; }
    }

}


// ViewModel composite, regroupe les ViewModel Vehicule, Achat, Annonce, Vente
// pour passer les objets à la vue.