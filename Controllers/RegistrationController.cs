using System;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;
using TourAgency.StaticClasses;

namespace TourAgency.Controllers {
    [Route("/registration")]
    public class RegistrationController : Controller {
        private readonly IUserService userService;

        public RegistrationController(IUserService userService) {
            this.userService = userService;
        }
        
        public IActionResult registration() {
            return View();
        }

        [HttpPost]
        public IActionResult registration(User user) {
            try {
                userService.createUser(user);
            } catch (Exception e) {
                ViewData.Add("exception", e.Message);
                return View();
            }

            return Redirect("/login?wat=" + Redirects.RegSuccess);
        }
        
    }
}