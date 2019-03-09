using Microsoft.EntityFrameworkCore;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public sealed class UserRepository : Repository<User>  {
        
        public UserRepository(TourAgencyDbContext dbContext) : base(dbContext) {}

        protected override DbSet<User> getDbSet() {
            return dbContext.users;
        }
    }
}