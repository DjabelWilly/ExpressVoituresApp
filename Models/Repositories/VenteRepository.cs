using ExpressVoituresApp.Data;
using ExpressVoituresApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Repositories
{
    public class VenteRepository : IVenteRepository
    {
        ApplicationDbContext _context;

        public VenteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddVenteAsync(Vente vente)
        {
            _context.Ventes.Add(vente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vente?>> GetVentesAsync()
        {
            return await _context.Ventes
                .Include(v => v.Vehicule)
                .OrderByDescending(v => v.Date)
                .ToListAsync();
        }

        public async Task<Vente?> GetVenteByVehiculeIdAsync(int vehiculeId)
        {
            return await _context.Ventes
                .FirstOrDefaultAsync(v => v.VehiculeId == vehiculeId);
        }

    }
}
