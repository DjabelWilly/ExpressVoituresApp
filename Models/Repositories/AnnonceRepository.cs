using ExpressVoituresApp.Data;
using ExpressVoituresApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Repositories
{
    public class AnnonceRepository : IAnnonceRepository
    {
        private readonly ApplicationDbContext _context;

        public AnnonceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Annonce>> GetAnnoncesDisponiblesAsync()
        {
            return await _context.Annonces
                .Include(a => a.Vehicule)
                .Where(a => a.Statut == "DISPONIBLE")
                .ToListAsync();
        }

        public async Task<Annonce?> GetAnnonceByIdAsync(int id)
        {
            return await _context.Annonces
                .Include(a => a.Vehicule)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAnnonceAsync(Annonce annonce)
        {
            _context.Annonces.Add(annonce);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnnonceAsync(Annonce annonce)
        {
            _context.Annonces.Update(annonce);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnnonceAsync(int id)
        {
            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce != null)
            {
                _context.Annonces.Remove(annonce);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
