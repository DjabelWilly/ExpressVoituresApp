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

        //----------------READ----------------//
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
        //-------------CREATE--------------//
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnnonceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? photoPath = null;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                // chemin relatif pour l’affichage dans <img src="">
                photoPath = "/images/" + fileName;
            }

            var annonce = new Annonce
            {
                Titre = model.Titre,
                Description = model.Description,
                Photo = photoPath, // <-- enregistre le chemin relatif
                Statut = "DISPONIBLE",
                Prix = model.Prix,
                VehiculeId = model.VehiculeId
            };

            await _annonceService.PublishAnnonceAsync(annonce);
            return RedirectToAction(nameof(Index));
        }


        //-------------UPDATE--------------//
        
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
                Prix = annonce.Prix,
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

            // Traitement du remplacement d’image
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                // Supprime l’ancienne image si elle existe
                if (!string.IsNullOrEmpty(annonce.Photo))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", annonce.Photo.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                annonce.Photo = "/images/" + fileName;
            }

            // Mise à jour des autres champs
            annonce.Titre = model.Titre;
            annonce.Description = model.Description;
            annonce.Statut = model.Statut;
            annonce.Prix = model.Prix;

            await _annonceService.UpdateAnnonceAsync(annonce);
            return RedirectToAction(nameof(Index));
        }


        //----------------DELETE-------------------//

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
