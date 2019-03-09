using Microsoft.EntityFrameworkCore;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public class TourRepository : Repository<Tour> {
        public TourRepository(TourAgencyDbContext dbContext) : base(dbContext) {}

        protected override DbSet<Tour> getDbSet() {
            return dbContext.tours;
        }
    }
}