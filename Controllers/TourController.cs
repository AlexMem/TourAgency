using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("/tours")]
    public class TourController : Controller {
        private readonly TourService tourService;
        private readonly UserService userService;
        private readonly TypeService typeService;

        public TourController(TourService tourService,
                              TypeService typeService,
                              UserService userService) {
            this.tourService = tourService;
            this.typeService = typeService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult activeTours(bool isHot) {
            return View(isHot?tourService.getAll():tourService.findByHot(isHot));
        }

        [HttpGet("/tours/myTours")]
        public IActionResult agentsTours() {
            return View(tourService.findByUser(userService.findByEmail(User.Identity.Name)));
        }
        
        [HttpGet("/tours/addTour")][Authorize(Roles = "agent")]
        public IActionResult addTour() {
            ViewData.Add("types", typeService.getAll());
            return View();
        }
        [HttpPost][Authorize(Roles = "agent")]
        public IActionResult addTour(Tour newTour) {
            tourService.create(newTour);
            return Redirect("/tours/myTours");
        }
        
        [HttpGet("/tours/myTours/change")][Authorize(Roles = "agent")]
        public IActionResult changeHotActive(long id) {
            return View(tourService.findById(id));
        }
        
        [HttpPost("/tours/myTours/change")][Authorize(Roles = "agent")]
        public IActionResult changeHotActive(Tour tour) {
            tourService.update(tour);
            return Redirect("/tours/myTours");
        }
    }
}
