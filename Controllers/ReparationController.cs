using ExpressVoituresApp.Models;
using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpressVoituresApp.Controllers
{
    [Authorize]
    public class ReparationController : Controller
    {
        private readonly IReparationService _reparationService;
        private readonly IVehiculeService _vehiculeService;

        public ReparationController(IReparationService reparationService, IVehiculeService vehiculeService)
        {
            _reparationService = reparationService;
            _vehiculeService = vehiculeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicules = await _reparationService.GetReparationsAsync();

            var model = vehicules.ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vehicules = await _vehiculeService.GetVehiculesAsync(); // récupère tous les véhicules
            ViewBag.Vehicules = vehicules
                .Select(v => new SelectListItem
                {
                    Value = v.Vehicule.Id.ToString(),
                    Text = $"{v.Vehicule.Marque} {v.Vehicule.Modele} ({v.Vehicule.Annee})"
                }).ToList();

            return View(new VehiculeReparationViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehiculeReparationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // re-remplir la liste des véhicules pour la vue
                var vehicules = await _vehiculeService.GetVehiculesAsync();
                ViewBag.Vehicules = vehicules
                    .Select(v => new SelectListItem
                    {
                        Value = v.Vehicule.Id.ToString(),
                        Text = $"{v.Vehicule.Marque} {v.Vehicule.Modele} ({v.Vehicule.Annee})"
                    }).ToList();

                return View(model);
            }

            // créer l'entité Reparation
            var reparation = new Reparation
            {
                Description = model.Reparation.Description,
                Cout = model.Reparation.Cout,
                VehiculeId = model.Vehicule.Id
            };

            await _reparationService.AddReparationAsync(reparation);

            return RedirectToAction(nameof(Index));
        }



    }
}
