using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Remotion.Linq.Clauses;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public sealed class UserRepository {

        public List<User> getAll() {
            List<User> users;
            using (var dbContext = new TourAgencyDbContext()) {
                users = dbContext.users
                                 .Include(u => u.role)
                                 .ToList();
            }
            return users;
        }
        
        public User findById(long id) {
            User user;
            using (var dbContext = new TourAgencyDbContext()) {
                user = dbContext.users
                                .Include(u => u.role)
                                .ToList()
                                .Find(u => u.userId == id);
            }
            return user;
        }

        public User findByEmail(string email) {
            User user;
            using (var dbContext = new TourAgencyDbContext()) {
                user = dbContext.users
                                .Include(u => u.role)
                                .ToList()
                                .Find(u => u.email.Equals(email));
            }
            return user;
        }
        
        
        public User create(User newUser) {
            EntityEntry<User> user;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.roles.Attach(newUser.role);
                user = dbContext.users.Add(newUser);
                dbContext.SaveChanges();
            }
            return user.Entity;
        }

        public User update(User user) {
            EntityEntry<User> updatedUser;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.roles.Attach(user.role);
                updatedUser = dbContext.users.Update(user);
                dbContext.SaveChanges();
            }
            return updatedUser.Entity;
        }

        public User delete(User user) {
            EntityEntry<User> removedUser;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.roles.Attach(user.role);
                removedUser = dbContext.users.Remove(user);
                dbContext.SaveChanges();
            }
            return removedUser.Entity;
        }
    }
}