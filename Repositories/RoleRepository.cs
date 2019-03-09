using Microsoft.EntityFrameworkCore;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public class RoleRepository : Repository<Role> {
        public RoleRepository(TourAgencyDbContext dbContext) : base(dbContext) {}

        protected override DbSet<Role> getDbSet() {
            return dbContext.roles;
        }
    }
}