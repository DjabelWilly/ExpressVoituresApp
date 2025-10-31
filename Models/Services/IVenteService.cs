using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;

namespace ExpressVoituresApp.Models.Services
{
    public interface IVenteService
    {
        Task AddVenteAsync(Vente vente);
        Task<IEnumerable<Vente?>> GetVentesAsync();
    }
}
