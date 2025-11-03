using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IAchatRepository
    {
        Task AddVehiculeAchatAsync(VehiculeAchatViewModel vehiculeAchat);
        Task<Achat?> GetAchatByVehiculeIdAsync(int vehiculeId);
        Task<bool> IsCodeVinExistAsync(string codeVin);
    }
}
