using ExpressVoituresApp.Models.ViewModels;

namespace ExpressVoituresApp.Models.Repositories
{
    public interface IAchatRepository
    {
        Task AddVehiculeAchatAsync(VehiculeAchatViewModel vehiculeAchat);

        Task<bool> IsCodeVinExistAsync(string codeVin);
    }
}
