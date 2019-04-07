using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Models.Messages;
using TourAgency.Services;
using TourAgency.StaticClasses;

namespace TourAgency.Controllers {
    [Route("/login")]
    public class LoginController : Controller {
        private readonly IUserService userService;

        public LoginController(IUserService userService) {
            this.userService = userService;
        }
        
        public IActionResult login(string wat) {
            if (User.Identity.IsAuthenticated) {
                return Redirect("/tours");
            }
            switch (wat) {
                case Redirects.RegSuccess:
                    return View(new Message("success", "Registered successful"));
            }

            return View();
        }
        
        [HttpGet("/logout")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/tours");
        }
        
        [HttpPost]
        public async Task<IActionResult> authentication(User user) {
            try {
                User foundUser = userService.authenticate(user);
                await authenticate(foundUser.email, foundUser.role.name);
            } catch (Exception e) {
                return View("login", new Message("error", e.Message));
            }

            return Redirect("/tours");
        }

        private async Task authenticate(string username, string role) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims,"ApplicationCookie");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
