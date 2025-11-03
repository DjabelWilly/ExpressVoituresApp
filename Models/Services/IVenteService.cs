using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Services
{
    public interface IVenteService
    {
        Task AddVenteAsync(Vente vente);
        Task<IEnumerable<Vente?>> GetVentesAsync();
        Task<Vente?> GetVenteByVehiculeIdAsync(int vehiculeId);

    }
}
