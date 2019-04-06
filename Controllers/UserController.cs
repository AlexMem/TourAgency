using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;
using TourAgency.StaticClasses;

namespace TourAgency.Controllers {
    [Route("/users")][Authorize(Roles = "admin, agent, customer")]
    public class UserController : Controller {
        private readonly UserService userService;

        public UserController(UserService userService) {
            this.userService = userService;
        }

        [HttpGet("/users/myInfo")]
        public IActionResult myInfo() {
            return View(userService.findByEmail(User.Identity.Name));
        }

        [HttpGet("/users/userInfo")][Authorize(Roles = "unknown")]
        public IActionResult userInfo(long userId) {
            return View(userService.findById(userId));
        }

        [HttpGet("/users/userInfo/change")][Authorize(Roles = "unknown")]
        public IActionResult changeUserInfo(long userId) {
            return View(userService.findById(userId));
        }
        
        [HttpPost("/users/myInfo")]
        public IActionResult changePassword(string newPassword) {
            User user = userService.findByEmail(User.Identity.Name);
            user.password = newPassword;
            try {
                User updatedUser = userService.update(user);
                ViewData.Add("message", "Password changed");
                return View("myInfo", updatedUser);
            } catch (Exception e) {
                ViewData.Add("exception", e.Message);
                return View("myInfo", user);
            }
        }
        
        [HttpGet][Authorize(Roles = "admin")]
        public IActionResult allUsers() {
            return View(userService.getAll());
        }

        [HttpGet("/users/createAgent")][Authorize(Roles = "admin")]
        public IActionResult createAgent() {
            return View();
        }
        
        [HttpPost("/users/createAgent")][Authorize(Roles = "admin")]
        public IActionResult createAgent(User user) {
            try {
                userService.createAgent(user);
                ViewData.Add("message", "New agent created");
                return Redirect("/users");
            } catch (Exception e) {
                ViewData.Add("exception", e.Message);
                return View();
            }
        }
        
        
        [HttpGet("{id}")][Authorize(Roles = "admin, agent")]
        public User getUser(int userId) {
            return userService.findById(userId);
        }

        [HttpPost][Authorize(Roles = "admin")]
        public User createUser([FromBody] User user) {
            return userService.create(user);
        }

        [HttpPut("{id}")][Authorize(Roles = "admin")]
        public User updateUser(long userId, [FromBody] User user) {
            user.userId = userId;
            return userService.update(user);
        }
        
        [HttpPost("/users/deleteUser")][Authorize(Roles = "admin")]
        public IActionResult deleteUser(long userId) {
            userService.delete(new User {userId = userId});
            return Redirect("/users");
        }
    }
}