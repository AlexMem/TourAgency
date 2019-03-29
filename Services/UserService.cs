using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class UserService {
        private static readonly Regex TelephoneVerificationPattern = new Regex("^\\+?\\d{1,4}\\d{10}$");
        private static readonly Regex EmailVerificationPattern = new Regex("^\\w+.?\\w{2,49}@[a-z]+\\.[a-z]+$");

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
            verify(newUser);
            newUser.password = Encoding.Unicode.GetString(SHA512.Create()
                                                                .ComputeHash(Encoding.Unicode.GetBytes(newUser.password)));
            
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
            if (string.IsNullOrEmpty(user.firstName) || string.IsNullOrEmpty(user.lastName)) {
                throw new Exception("Input your first name and last name");
            }
            
            emailValidation(user);
            passwordValidation(user.password);
            telephoneValidation(user.telephone);
        }

        private void passwordValidation(string password) {
            if (string.IsNullOrEmpty(password)) {
                throw new Exception("Input your password");
            }

            if (password.Length < 5) {
                throw new Exception("Create password longer than 4 characters");
            }
        }
        private void telephoneValidation(string telephone) {
            if (string.IsNullOrEmpty(telephone)) {
                throw new Exception("Input your telephone");
            }
            if (!TelephoneVerificationPattern.IsMatch(telephone)) {
                throw new Exception("Incorrect telephone number");
            }
        }
        private void emailValidation(User user) {
            if (string.IsNullOrEmpty(user.email)) {
                throw new Exception("Input your email");
            }
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
