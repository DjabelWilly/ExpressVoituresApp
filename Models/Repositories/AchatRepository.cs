using System.Threading.Tasks;
using ExpressVoituresApp.Data;
using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Repositories
{
    public class AchatRepository : IAchatRepository
    {
        private readonly ApplicationDbContext _context;

        public AchatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddVehiculeAchatAsync(VehiculeAchatViewModel vehiculeAchat)
        {
            // Split le VehiculeAchatViewModel en Vehicule et Achat et les enregistre en base
            var vehicule = new Vehicule
            {
                CodeVin = vehiculeAchat.Vehicule.CodeVin,
                Marque = vehiculeAchat.Vehicule.Marque,
                Modele = vehiculeAchat.Vehicule.Modele,
                Finition = vehiculeAchat.Vehicule.Finition,
                Annee = vehiculeAchat.Vehicule.Annee
            };

            var achat = new Achat
            {
                Vehicule = vehicule,
                Date = vehiculeAchat.Achat.Date,
                Prix = vehiculeAchat.Achat.Prix
            };

            _context.Add(achat);
            await _context.SaveChangesAsync();

        }

        public async Task<Achat?> GetAchatByVehiculeIdAsync(int vehiculeId)
        {
            return await _context.Achats
                .Include(a => a.Vehicule)
                .FirstOrDefaultAsync(a => a.VehiculeId == vehiculeId);
        }


        public async Task<bool> IsCodeVinExistAsync(string codeVin)
        {
            return await _context.Vehicules.AnyAsync(v=> v.CodeVin.ToLower() == codeVin.ToLower());
        }
    }
}
