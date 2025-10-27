using ExpressVoituresApp.Models;
using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpressVoituresApp.Controllers
{
    public class AnnonceController : Controller
    {
        private readonly IAnnonceService _annonceService;
        private readonly IVehiculeService _vehiculeService;

        public AnnonceController(IAnnonceService annonceService, IVehiculeService vehiculeService)
        {
            _annonceService = annonceService;
            _vehiculeService = vehiculeService;
        }

        // ==========================
        // VISITEURS PUBLICS
        // ==========================
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var annonces = await _annonceService.GetAnnoncesDisponiblesAsync();
            return View(annonces);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var annonce = await _annonceService.GetByIdAsync(id);
            if (annonce == null) return NotFound();
            return View(annonce);
        }

        // ==========================
        // ADMIN
        // ==========================
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var vehicules = await _vehiculeService.GetVehiculesWithoutAnnonceAsync();

            ViewBag.Vehicules = new SelectList(
                vehicules.Select(v => new
                {
                    v.Vehicule.Id,
                    Display = $"{v.Vehicule.Marque} {v.Vehicule.Modele} ({v.Vehicule.Annee})"
                }),
                "Id", "Display"
            );

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnnonceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vehicules = await _vehiculeService.GetVehiculesWithoutAnnonceAsync();
                ViewBag.Vehicules = new SelectList(
                    vehicules.Select(v => new
                    {
                        v.Vehicule.Id,
                        Display = $"{v.Vehicule.Marque} {v.Vehicule.Modele} ({v.Vehicule.Annee})"
                    }),
                    "Id", "Display"
                );
                return View(model);
            }

            var annonce = new Annonce
            {
                Titre = model.Titre,
                Description = model.Description,
                Photo = model.Photo,
                Statut = "DISPONIBLE",
                VehiculeId = model.VehiculeId
            };

            await _annonceService.PublishAnnonceAsync(annonce);
            return RedirectToAction(nameof(Index));
        }


        // Récupère l'annonce à modifier
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var annonce = await _annonceService.GetByIdAsync(id);
            if (annonce == null) return NotFound();

            var model = new AnnonceViewModel
            {
                Id = annonce.Id,
                Titre = annonce.Titre,
                Description = annonce.Description,
                Photo = annonce.Photo,
                Statut = annonce.Statut,
                VehiculeId = annonce.VehiculeId
            };

            return View(model);
        }

        // Valide et sauvegarde les modifications en base
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnnonceViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var annonce = await _annonceService.GetByIdAsync(model.Id);
            if (annonce == null) return NotFound();

            annonce.Titre = model.Titre;
            annonce.Description = model.Description;
            annonce.Photo = model.Photo;
            annonce.Statut = model.Statut;

            await _annonceService.UpdateAnnonceAsync(annonce);
            return RedirectToAction(nameof(Index));
        }

        // Récupère une annonce et la supprime
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var annonce = await _annonceService.GetByIdAsync(id);
            if (annonce == null) return NotFound();
            return View(annonce);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _annonceService.DeleteAnnonceAsync(id);

            TempData["SuccessMessage"] = "Suppression réussi !";

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MarquerCommeVendue(int id)
        {
            await _annonceService.MarkAsSoldAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
