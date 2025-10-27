using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Services
{
    public interface IAnnonceService
    {
        Task<IEnumerable<Annonce>> GetAnnoncesDisponiblesAsync();
        Task<Annonce?> GetByIdAsync(int id);
        Task PublishAnnonceAsync(Annonce annonce);
        Task MarkAsSoldAsync(int id);
        Task UpdateAnnonceAsync(Annonce annonce);
        Task DeleteAnnonceAsync(int id);
    }
}
