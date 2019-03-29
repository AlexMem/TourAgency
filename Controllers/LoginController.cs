using Microsoft.AspNetCore.Mvc;
using TourAgency.Models.Messages;
using TourAgency.StaticClasses;

namespace TourAgency.Controllers {
    [Route("/login")]
    public class LoginController : Controller {

        public IActionResult login(string wat) {
            switch (wat) {
                case Redirects.RegSuccess:
                    return View(new Message("success", "Registered successful"));
            }

            return View();
        }
    }
}