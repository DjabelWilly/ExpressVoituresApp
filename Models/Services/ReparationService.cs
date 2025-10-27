using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;

namespace ExpressVoituresApp.Models.Services
{
    public class ReparationService : IReparationService
    {
        private readonly IReparationRepository _reparationRepository;

        public ReparationService(IReparationRepository reparationRepository)
        {
            _reparationRepository = reparationRepository;
        }

        public async Task<IEnumerable<VehiculeReparationViewModel>> GetReparationsAsync()
        {
            return await _reparationRepository.GetReparationsAsync();
        }

        public async Task AddReparationAsync(Reparation reparation)
        {
            await _reparationRepository.AddAsync(reparation);
        }

    }
}
