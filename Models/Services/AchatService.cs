using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;
using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Services
{
    public class AchatService : IAchatService
    {
        private readonly IAchatRepository _achatRepository;
        private readonly IVehiculeRepository _vehiculeRepository;

        public AchatService(IAchatRepository vehiculeAchatRepository, IVehiculeRepository vehiculeRepository)
        {
            _achatRepository = vehiculeAchatRepository;
            _vehiculeRepository = vehiculeRepository;
        }

        public async Task AddVehiculeAchatAsync(VehiculeAchatViewModel vehiculeAchat)
        {
            if (await _achatRepository.IsCodeVinExistAsync(vehiculeAchat.Vehicule.CodeVin))
            {
                throw new InvalidOperationException("Ce CodeVIN existe déjà.");
            }

            await _achatRepository.AddVehiculeAchatAsync(vehiculeAchat);
        }

        public async Task<Achat?> GetAchatByVehiculeIdAsync(int vehiculeId)
        {
            return await _achatRepository.GetAchatByVehiculeIdAsync(vehiculeId);
        }
    }
}

