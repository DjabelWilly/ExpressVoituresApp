using ExpressVoituresApp.Models;
using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Services;
using ExpressVoituresApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoituresApp.Controllers
{
    [Authorize]
    public class VehiculeController : Controller
    {
        private readonly IVehiculeService _vehiculeService;

        public VehiculeController(IVehiculeService vehiculeService)
        {
            _vehiculeService = vehiculeService;
        }

        // Récupère tous les véhicules de la DB
        public async Task<IActionResult> Index()
        {
            var vehicules = await _vehiculeService.GetVehiculesAsync();
            return View(vehicules);
        }

        // GET : afficher la confirmation de suppression
        public async Task<IActionResult> Delete(int id)
        {
            var vehicule = await _vehiculeService.GetVehiculeByIdAsync(id);

            if (vehicule == null)
                return NotFound();

            // Mapping vers le ViewModel
            var vm = new VehiculeAchatViewModel
            {
                Vehicule = new VehiculeViewModel
                {
                    Id = vehicule.Id,
                    CodeVin = vehicule.CodeVin,
                    Marque = vehicule.Marque,
                    Modele = vehicule.Modele,
                    Finition = vehicule.Finition,
                    Annee = vehicule.Annee
                },
                Achat = vehicule.Achat != null ? new AchatViewModel
                {
                    Date = vehicule.Achat.Date,
                    Prix = vehicule.Achat.Prix
                } : null!
            };

            return View(vm);
        }

        // POST : suppression effective
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehiculeService.DeleteVehiculeAsync(id);

            TempData["SuccessMessage"] = "Suppression réussie !";

            return RedirectToAction(nameof(Index));
        }
    }
}
