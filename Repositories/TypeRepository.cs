using Microsoft.EntityFrameworkCore;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public class TypeRepository : Repository<Type> {
        public TypeRepository(TourAgencyDbContext dbContext) : base(dbContext) {}

        protected override DbSet<Type> getDbSet() {
            return dbContext.types;
        }
    }
}