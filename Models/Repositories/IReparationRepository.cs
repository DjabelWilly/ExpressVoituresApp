using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IReparationRepository
    {
        public Task<IEnumerable<VehiculeReparationViewModel>> GetReparationsAsync();

        public Task AddAsync(Reparation reparation);
    }
}
