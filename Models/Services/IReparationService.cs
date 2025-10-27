using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Services
{
    public interface IReparationService
    {
        public Task<IEnumerable<VehiculeReparationViewModel>> GetReparationsAsync();

        public Task AddReparationAsync(Reparation reparation);
    }
}
