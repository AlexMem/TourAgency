using Microsoft.AspNetCore.Mvc;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("/tours")]
    public class TourController : Controller {
        private readonly TourService tourService;

        public TourController(TourService tourService) {
            this.tourService = tourService;
        }

        [HttpGet]
        public IActionResult activeTours() {
            return View(tourService.getAll());
        }
    }
}