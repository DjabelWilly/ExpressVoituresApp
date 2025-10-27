using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Services
{
    public interface IAchatService
    {
        Task AddVehiculeAchatAsync(VehiculeAchatViewModel vehiculeAchat );
    }
}
