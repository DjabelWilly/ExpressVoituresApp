using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;
using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Services
{
    public class VehiculeService : IVehiculeService
    {
        private readonly IVehiculeRepository _vehiculeRepository;

        public VehiculeService(IVehiculeRepository vehiculeRepository)
        {
            _vehiculeRepository = vehiculeRepository;
        }

        public async Task<IEnumerable<VehiculeAchatViewModel>> GetVehiculesAsync()
        {
            return await _vehiculeRepository.GetVehiculesAsync();
        }

        public async Task<IEnumerable<VehiculeAchatViewModel>> GetVehiculesWithoutAnnonceAsync()
        {
            return await _vehiculeRepository.GetVehiculesWithoutAnnonceAsync();
        }

        public async Task<Vehicule> GetVehiculeByIdAsync(int id)
        {
            return await _vehiculeRepository.GetVehiculeByIdAsync(id);
        }

        public async Task DeleteVehiculeAsync(int id)
        {
            await _vehiculeRepository.DeleteVehiculeAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _vehiculeRepository.SaveChangesAsync();
        }
    }
}
