using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories.Implementations {
    public class RoleRepository : IRoleRepository {
      
        public List<Role> getAll() {
            List<Role> roles;
            using (var dbContext = new TourAgencyDbContext()) {
                roles = dbContext.roles.ToList();
            }
            return roles;
        }
        
        public Role findById(long id) {
            Role role;
            using (var dbContext = new TourAgencyDbContext()) {
                role = dbContext.roles
                                .ToList()
                                .Find(r => r.roleId == id);
            }
            return role;
        }
        
        public Role findByName(string name) {
            Role role;
            using (var dbContext = new TourAgencyDbContext()) {
                role = dbContext.roles
                                .ToList()
                                .Find(r => r.name.Equals(name));
            }
            return role;
        }
        
        public Role create(Role newRole) {
            EntityEntry<Role> role;
            using (var dbContext = new TourAgencyDbContext()) {
                role = dbContext.roles.Add(newRole);
                dbContext.SaveChanges();
            }
            return role.Entity;
        }

        public Role update(Role role) {
            EntityEntry<Role> updatedRole;
            using (var dbContext = new TourAgencyDbContext()) {
                updatedRole = dbContext.roles.Update(role);
                dbContext.SaveChanges();
            }
            return updatedRole.Entity;
        }

        public Role delete(Role role) {
            EntityEntry<Role> removedRole;
            using (var dbContext = new TourAgencyDbContext()) {
                removedRole = dbContext.roles.Remove(role);
                dbContext.SaveChanges();
            }
            return removedRole.Entity;
        }
    }
}
