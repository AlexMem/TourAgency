using Microsoft.EntityFrameworkCore;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public sealed class OrderRepository : Repository<Order> {
        public OrderRepository(TourAgencyDbContext dbContext) : base(dbContext) {}

        protected override DbSet<Order> getDbSet() {
            return dbContext.orders;
        }
    }
}