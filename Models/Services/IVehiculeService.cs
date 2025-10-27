using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Services
{
    public interface IVehiculeService
    {
        Task<IEnumerable<VehiculeAchatViewModel>> GetVehiculesAsync();
        Task<IEnumerable<VehiculeAchatViewModel>> GetVehiculesWithoutAnnonceAsync();
        Task<Vehicule> GetVehiculeByIdAsync(int id);
        Task DeleteVehiculeAsync(int id);
        Task SaveChangesAsync();
    }
}
