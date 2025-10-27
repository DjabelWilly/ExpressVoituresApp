using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;

namespace ExpressVoituresApp.Models.Services
{
    public class AnnonceService : IAnnonceService
    {
        private readonly IAnnonceRepository _annonceRepository;

        public AnnonceService(IAnnonceRepository annonceRepository)
        {
            _annonceRepository = annonceRepository;
        }

        public async Task<IEnumerable<Annonce>> GetAnnoncesDisponiblesAsync()
        {
            return await _annonceRepository.GetAnnoncesDisponiblesAsync();
        }

        public async Task<Annonce?> GetByIdAsync(int id)
        {
            return await _annonceRepository.GetAnnonceByIdAsync(id);
        }

        public async Task PublishAnnonceAsync(Annonce annonce)
        {
            // Règle métier : statut = DISPONIBLE
            annonce.Statut = "DISPONIBLE";
            await _annonceRepository.AddAnnonceAsync(annonce);
            await _annonceRepository.SaveChangesAsync();
        }

        public async Task MarkAsSoldAsync(int id)
        {
            var annonce = await _annonceRepository.GetAnnonceByIdAsync(id);
            if (annonce == null)
                throw new KeyNotFoundException($"Annonce {id} introuvable.");

            annonce.Statut = "VENDU";
            await _annonceRepository.UpdateAnnonceAsync(annonce);
            await _annonceRepository.SaveChangesAsync();
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
