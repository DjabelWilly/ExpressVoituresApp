using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IAnnonceRepository
    {
        Task<IEnumerable<Annonce>> GetAnnoncesDisponiblesAsync();
        Task<Annonce?> GetAnnonceByIdAsync(int id);
        Task AddAnnonceAsync(Annonce annonce);
        Task UpdateAnnonceAsync(Annonce annonce);
        Task DeleteAnnonceAsync(int id);
        Task SaveChangesAsync();
    }
}
