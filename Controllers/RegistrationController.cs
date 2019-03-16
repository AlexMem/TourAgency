using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TourAgency.Models;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("/registration")]
    public class RegistrationController : Controller {
        private readonly UserService userService;

        public RegistrationController(UserService userService) {
            this.userService = userService;
        }
        
        public IActionResult registration() {
            return View();
        }

        [HttpPost]
        public IActionResult register(User user) {
            userService.createUser(user);
            return Redirect("/login");
        }
        
    }
}