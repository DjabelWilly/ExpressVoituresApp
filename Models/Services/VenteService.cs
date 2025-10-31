using ExpressVoituresApp.Models.Entities;

namespace ExpressVoituresApp.Models.Services
{
    public class VenteService : IVenteService

    {
        private readonly IVenteService _venteRepository;

        public VenteService(IVenteService venteRepository)
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
    }
}

