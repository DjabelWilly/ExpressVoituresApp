using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Services
{
    public class VenteService : IVenteService
    {
        private readonly IVenteRepository _venteRepository;

        public VenteService(IVenteRepository venteRepository)
        {
            _venteRepository = venteRepository;
        }

        public async Task AddVenteAsync(Vente vente)
        {
            await _venteRepository.AddVenteAsync(vente);
        }

        public async Task<IEnumerable<Vente?>> GetVentesAsync()
        {
            return await _venteRepository.GetVentesAsync();
        }

        public async Task<Vente?> GetVenteByVehiculeIdAsync(int vehiculeId)
        {
            return await _venteRepository.GetVenteByVehiculeIdAsync(vehiculeId);

        }
    }
}


