using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("api/[controller]")]
    public class UserController : Controller {
        private readonly UserService userService;

        public UserController(UserService userService) {
            this.userService = userService;
        }

        [HttpGet][Authorize(Roles = "admin")]
        public List<User> getAllUsers() {
            return userService.getAll();
        }

        [HttpGet("{id}")][Authorize(Roles = "admin, agent")]
        public User getUser(int id) {
            return userService.get(new object[]{id});
        }

        [HttpPost][Authorize(Roles = "admin")]
        public User createUser([FromBody] User user) {
            return userService.create(user);
        }

        [HttpPut("{id}")][Authorize(Roles = "admin")]
        public User updateUser(int id, [FromBody] User user) {
            user.userId = id;
            return userService.update(user);
        }
        
        [HttpDelete("{id}")][Authorize(Roles = "admin")]
        public User deleteUser(int id) {
            return userService.delete(new User {userId = id});
        }
    }
}