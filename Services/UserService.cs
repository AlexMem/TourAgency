using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class UserService {
        private static readonly Regex TelephoneVerificationPattern = new Regex("^\\+?\\d{1,4}\\d{10}$");
        private static readonly Regex EmailVerificationPattern = new Regex("^\\w+.?\\w{2,49}+@[a-z]+\\.[a-z]+$");

        private readonly UserRepository userRepository;
        private readonly RoleService roleService;

        public UserService(UserRepository userRepository, 
                           RoleService roleService) {
            this.userRepository = userRepository;
            this.roleService = roleService;
        }

        public List<User> getAll() {
            return userRepository.getAll();
        }

        public User get(object[] keyValues) {
            User user = userRepository.get(keyValues);
            if (user == null) {
                //TODO
                throw new Exception("User not found");
            }

            return user;
        }

        public User createUser(User newUser) {
            newUser.role = roleService.findByName("user");
            return create(newUser);
        }

        public User createAgent(User newUser) {
            newUser.role = roleService.findByName("agent");
            return create(newUser);
        }

        public User createAdmin(User newUser) {
            newUser.role = roleService.findByName("admin");
            return create(newUser);
        }

        public User create(User newUser) {
//            verify(newUser);
            return userRepository.create(newUser);
        }

        public User update(User user) {
//            verify(user);
            return userRepository.update(user);
        }

        public User delete(User user) {
            return userRepository.delete(user);
        }

        public void verify(User user) {
            if (user.firstName.Length == 0 || user.lastName.Length == 0) {
                throw new Exception("Input your first name and last name");
            }
            
            emailValidation(user);
            passwordValidation(user.password);
            telephoneValidation(user.telephone);
        }

        private void passwordValidation(string password) {
            if (password.Length == 0) {
                throw new Exception("Input your password");
            }

            if (password.Length < 5) {
                throw new Exception("Create password longer than 4 characters");
            }
        }
        private void telephoneValidation(string telephone) {
            if (!TelephoneVerificationPattern.IsMatch(telephone)) {
                throw new Exception("Incorrect telephone number");
            }
        }
        private void emailValidation(User user) {
            if (!EmailVerificationPattern.IsMatch(user.email)) {
                throw new Exception("Incorrect email");
            }
            
            userRepository.getAll().ForEach(innerUser => {
                if (innerUser.userId != user.userId && innerUser.email.Equals(user.email)) {
                    throw new Exception("This email is registered already");
                }
            });
        }
    }
}
