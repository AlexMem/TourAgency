using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class UserService {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository) {
            this.userRepository = userRepository;
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

        public User create(User newUser) {
            verify(newUser);
            return userRepository.create(newUser);
        }

        public User update(User user) {
            verify(user);
            return userRepository.update(user);
        }

        public User delete(User user) {
            return userRepository.delete(user);
        }

        public void verify(User user) {
            //TODO verification
            throw new NotImplementedException();
        }
    }
}