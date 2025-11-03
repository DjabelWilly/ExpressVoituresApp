using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IVenteRepository
    {
        Task AddVenteAsync(Vente vente);
        Task<IEnumerable<Vente?>> GetVentesAsync(); 
        Task<Vente?> GetVenteByVehiculeIdAsync(int vehiculeId);

    }
}
