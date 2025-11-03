using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Services
{
    public interface IAchatService
    {
        Task AddVehiculeAchatAsync(VehiculeAchatViewModel vehiculeAchat );

        Task<Achat?> GetAchatByVehiculeIdAsync(int vehiculeId);
    }
}
