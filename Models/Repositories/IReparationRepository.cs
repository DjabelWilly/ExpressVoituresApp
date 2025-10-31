using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IReparationRepository
    {
        public Task<IEnumerable<VehiculeReparationViewModel>> GetReparationsAsync();

        Task<decimal> GetReparationTotalByVehiculeIdAsync(int vehiculeId);

        public Task AddAsync(Reparation reparation);
    }
}
