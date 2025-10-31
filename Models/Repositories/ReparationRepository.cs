using ExpressVoituresApp.Data;
using ExpressVoituresApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Repositories
{
    public class ReparationRepository : IReparationRepository
    {
        private readonly ApplicationDbContext _context;
        public ReparationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehiculeReparationViewModel>> GetReparationsAsync()
        {
            return await _context.Reparations
                .Include(r => r.Vehicule)
                .Select(r => new VehiculeReparationViewModel
                {
                    Reparation = new ReparationViewModel
                    {
                        Id = r.Id,
                        Description = r.Description,
                        Cout = r.Cout
                    },
                    Vehicule = r.Vehicule != null ? new VehiculeViewModel
                    {
                        Id = r.Vehicule.Id,
                        CodeVin = r.Vehicule.CodeVin,
                        Marque = r.Vehicule.Marque,
                        Modele = r.Vehicule.Modele,
                        Finition = r.Vehicule.Finition,
                        Annee = r.Vehicule.Annee
                    } : null!
                })
                  .ToListAsync();
        }

        public async Task<decimal> GetReparationTotalByVehiculeIdAsync(int vehiculeId)
        {
            return await _context.Reparations
                .Where(r => r.VehiculeId == vehiculeId)
                .SumAsync(r => (decimal?)r.Cout) ?? 0;
        }


        public async Task AddAsync(Reparation reparation)
        {
            _context.Reparations.Add(reparation);
            await _context.SaveChangesAsync();
        }


    }
}
