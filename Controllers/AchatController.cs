using ExpressVoituresApp.Models.Entities;
using ExpressVoituresApp.Models.Repositories;
using ExpressVoituresApp.Models.Services;
using ExpressVoituresApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoituresApp.Controllers
{
    public class AchatController : Controller
    {
        private readonly IAchatService _achatService;

        public AchatController(IAchatService achatService)
        {
            _achatService = achatService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehiculeAchatViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {

                var vehicule = new Vehicule
                {
                    CodeVin = model.Vehicule.CodeVin,
                    Marque = model.Vehicule.Marque,
                    Modele = model.Vehicule.Modele,
                    Finition = model.Vehicule.Finition,
                    Annee = model.Vehicule.Annee,
                };

                var achat = new Achat
                {
                    Date = model.Achat.Date,
                    Prix = model.Achat.Prix
                };

                TempData["SuccessMessage"] = "Enregistrement réussi !";

                await _achatService.AddVehiculeAchatAsync(model);
                return RedirectToAction("Index", "Vehicule");

            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Vehicule.CodeVin", ex.Message);
                return View(model);
            }

        }
    }

}
