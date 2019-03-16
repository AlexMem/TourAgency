using Microsoft.AspNetCore.Mvc;

namespace TourAgency.Controllers {
    [Route("/login")]
    public class LoginController : Controller {
        
        public IActionResult login() {
            return View();
        }
    }
}