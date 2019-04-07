using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("/tours")]
    public class TourController : Controller {
        private readonly ITourService tourService;
        private readonly IUserService userService;
        private readonly ITypeService typeService;

        public TourController(ITourService tourService,
                              ITypeService typeService,
                              IUserService userService) {
            this.tourService = tourService;
            this.typeService = typeService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult activeTours(bool isHot) {
            return View(isHot?tourService.findByHot(isHot):tourService.getAll());
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
        [HttpPost("/tours/addTour")][Authorize(Roles = "agent")]
        public IActionResult addTour(Tour newTour, string type) {
            try {
                newTour.type = typeService.findByName(type);
                newTour.user = userService.findByEmail(User.Identity.Name);
                tourService.create(newTour);
                ViewData.Add("message", "Tour created");
            } catch (Exception e) {
                ViewData.Add("exception", e.Message);
                return View();
            }

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
