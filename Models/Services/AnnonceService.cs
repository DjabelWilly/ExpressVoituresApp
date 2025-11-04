using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;

namespace ExpressVoituresApp.Models.Services
{
    public class AnnonceService : IAnnonceService
    {
        private readonly IAnnonceRepository _annonceRepository;
        private readonly IVenteService _venteService;

        public AnnonceService(IAnnonceRepository annonceRepository, IVenteService venteService)
        {
            _annonceRepository = annonceRepository;
            _venteService = venteService;
        }

        public async Task<IEnumerable<Annonce>> GetAnnoncesDisponiblesAsync()
        {
            return await _annonceRepository.GetAnnoncesDisponiblesAsync();
        }

        public async Task<Annonce?> GetAnnonceByIdAsync(int id)
        {
            return await _annonceRepository.GetAnnonceByIdAsync(id);
        }

        public async Task PublishAnnonceAsync(Annonce annonce)
        {
            annonce.Statut = "DISPONIBLE";
            await _annonceRepository.AddAnnonceAsync(annonce);
            await _annonceRepository.SaveChangesAsync();
        }

        public async Task MarkAsSoldAsync(int id)
        {
            var annonce = await _annonceRepository.GetAnnonceByIdAsync(id);
            if (annonce == null)
                throw new KeyNotFoundException($"Annonce {id} introuvable.");

            // Vérifie si une vente existe déjà pour ce véhicule
            var existingVente = await _venteService.GetVenteByVehiculeIdAsync(annonce.VehiculeId);
            if (existingVente != null)
                return; // Vente déjà enregistrée, ne rien faire

            // Change l'annonce en statut "VENDU"
            annonce.Statut = "VENDU";
            await _annonceRepository.UpdateAnnonceAsync(annonce);
            await _annonceRepository.SaveChangesAsync();

            // Ajoute la vente
            await _venteService.AddVenteAsync(new Vente
            {
                VehiculeId = annonce.VehiculeId,
                Date = DateTime.Now,
                Prix = annonce.Prix
            });
        }


        public async Task UpdateAnnonceAsync(Annonce annonce)
        {
            var existing = await _annonceRepository.GetAnnonceByIdAsync(annonce.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Annonce {annonce.Id} introuvable.");

            // Règle métier : seuls certains champs sont modifiables
            existing.Titre = annonce.Titre;
            existing.Description = annonce.Description;
            existing.Photo = annonce.Photo;
            existing.Statut = annonce.Statut;

            await _annonceRepository.UpdateAnnonceAsync(existing);
            await _annonceRepository.SaveChangesAsync();
        }

        public async Task DeleteAnnonceAsync(int id)
        {
            await _annonceRepository.DeleteAnnonceAsync(id);
            await _annonceRepository.SaveChangesAsync();
        }
    }
}
