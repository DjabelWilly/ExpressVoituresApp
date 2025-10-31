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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult create()
        {
            return View();
        }
    }
}
