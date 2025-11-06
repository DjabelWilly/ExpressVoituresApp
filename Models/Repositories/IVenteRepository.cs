using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IVenteRepository
    {
        Task AddVenteAsync(Vente vente);
        Task<IEnumerable<Vente?>> GetVentesAsync(); 
        Task<Vente?> GetVenteByVehiculeIdAsync(int vehiculeId);

    }
}
