using ExpressVoituresApp.Data;
using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoituresApp.Models.Repositories
{
    public class VehiculeRepository : IVehiculeRepository
    {
        private readonly ApplicationDbContext _context;

        public VehiculeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehiculeAchatViewModel>> GetVehiculesAsync()
        {
            return await _context.Vehicules
                .Include(v => v.Achat)
                .Include(v => v.Annonce)
                .Include(v => v.Vente)
                .Include(v => v.Reparations)
                .Select(v => new VehiculeAchatViewModel
                {
                    Vehicule = new VehiculeViewModel
                    {
                        Id = v.Id,
                        CodeVin = v.CodeVin,
                        Marque = v.Marque,
                        Modele = v.Modele,
                        Finition = v.Finition,
                        Annee = v.Annee
                    },

                    Achat = v.Achat != null ? new AchatViewModel
                    {
                        Date = v.Achat.Date,
                        Prix = v.Achat.Prix
                    } : null!,

                    Annonce = v.Annonce != null ? new AnnonceViewModel
                    {
                        Id = v.Annonce.Id,
                        Titre = v.Annonce.Titre,
                        Description = v.Annonce.Description,
                        Statut = v.Annonce.Statut,
                        Prix = v.Annonce.Prix
                    } : null!,

                    Vente = v.Vente != null ? new VenteViewModel
                    {
                        Id = v.Vente.Id,
                        Date = v.Vente.Date,
                        Prix = v.Vente.Prix
                    } : null,

                    TotalReparations = v.Reparations.Sum(r => r.Cout)
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<VehiculeAchatViewModel>> GetVehiculesWithoutAnnonceAsync()
        {
            return await _context.Vehicules
                .Include(v => v.Achat)
                .Include(v => v.Annonce)
                .Where(v => v.Annonce == null)
                .Select(v => new VehiculeAchatViewModel
                {
                    Vehicule = new VehiculeViewModel
                    {
                        Id = v.Id,
                        CodeVin = v.CodeVin,
                        Marque = v.Marque,
                        Modele = v.Modele,
                        Finition = v.Finition,
                        Annee = v.Annee
                    }
                })
                .ToListAsync();
        }


        public async Task<Vehicule> GetVehiculeByIdAsync(int id)
        {
            return await _context.Vehicules
                .Include(v => v.Annonce)
                .Include(v => v.Achat)
                .Include(v => v.Vente)
                .Include(v => v.Reparations)
                .FirstAsync(v => v.Id == id);
        }

        public async Task DeleteVehiculeAsync(int id)
        {
            var vehicule = await _context.Vehicules.FindAsync(id);

            _context.Vehicules.Remove(vehicule);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
