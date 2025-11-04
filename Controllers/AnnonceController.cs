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
        private readonly IReparationService _reparationService;
        private readonly IAchatService _achatService;
        private readonly IVenteService _venteService;

        public AnnonceController(
            IAnnonceService annonceService,
            IVehiculeService vehiculeService,
            IReparationService reparationService,
            IAchatService achatService,
            IVenteService venteService
            )
        {
            _annonceService = annonceService;
            _vehiculeService = vehiculeService;
            _reparationService = reparationService;
            _achatService = achatService;
            _venteService = venteService;
        }

        // ==========================
        // VISITEURS PUBLICS
        // ==========================

        //----------------READ----------------
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var annonces = await _annonceService.GetAnnoncesDisponiblesAsync();
            return View(annonces);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var annonce = await _annonceService.GetAnnonceByIdAsync(id);
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
        // Retourne dynamiquement le prix calculé dans le navigateur.
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPrixCalcule(int vehiculeId)
        {
            var achat = await _achatService.GetAchatByVehiculeIdAsync(vehiculeId);
            if (achat == null) return Json(0);

            var coutReparations = await _reparationService.GetReparationTotalByVehiculeIdAsync(vehiculeId);
            var prixVente = (int)(achat.Prix + coutReparations + 500);

            return Json(prixVente);
        }


        //-------------CREATE--------------
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

            // règle métier : Prix de vente = prix achat + coût réparations + 500
            var achat = await _achatService.GetAchatByVehiculeIdAsync(model.VehiculeId);
            if (achat == null) return View(model);

            decimal? coutReparations = await _reparationService.GetReparationTotalByVehiculeIdAsync(model.VehiculeId);

            var prixVente = achat.Prix + coutReparations + 500;

            var annonce = new Annonce
            {
                Titre = model.Titre,
                Description = model.Description,
                Photo = photoPath,
                Statut = "DISPONIBLE",
                Prix = (int)prixVente,
                VehiculeId = model.VehiculeId
            };

            await _annonceService.PublishAnnonceAsync(annonce);
            return RedirectToAction(nameof(Index));
        }

        //-------------UPDATE--------------

        // Récupère l'annonce à modifier
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var annonce = await _annonceService.GetAnnonceByIdAsync(id);
            if (annonce == null) return NotFound();

            var model = new AnnonceViewModel
            {
                Id = annonce.Id,
                Titre = annonce.Titre,
                Description = annonce.Description,
                Photo = annonce.Photo,
                Statut = annonce.Statut,
                Prix = annonce.Prix,
                DatePublication = annonce.DatePublication,
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

            var annonce = await _annonceService.GetAnnonceByIdAsync(model.Id);

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
            annonce.DatePublication = model.DatePublication;

            // statut == 'vendu', on crée une vente
            if (model.Statut == "VENDU")
            {
                await _annonceService.MarkAsSoldAsync(model.Id);
            }
            else
            {
                await _annonceService.UpdateAnnonceAsync(annonce);
            }

            TempData["SuccessMessage"] = "Enregistrement vente réussi !";

            return RedirectToAction("Index", "Vente");
        }


        //----------------DELETE-------------------

        // Récupère une annonce et la supprime
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var annonce = await _annonceService.GetAnnonceByIdAsync(id);
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
