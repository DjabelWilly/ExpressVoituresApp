namespace ExpressVoituresApp.Models.ViewModels
{
    public class VehiculeAchatViewModel
    {
        public VehiculeViewModel Vehicule { get; set; } = null!;
        public AchatViewModel Achat { get; set; } = null!;
        public AnnonceViewModel? Annonce { get; set; } = null !;
    }

}


// ViewModel composite, regroupe Achat, Vehicule et Annonce
// pour passer les objets aux vues.