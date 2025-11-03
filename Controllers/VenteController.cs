using ExpressVoituresApp.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoituresApp.Controllers
{
    [Authorize]
    public class VenteController : Controller
    {
        private readonly IVenteService _venteService;

        public VenteController(IVenteService venteService)
        {
            _venteService = venteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ventes = await _venteService.GetVentesAsync();
            return View(ventes);
        }

    }
}
