using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories.Implementations {
    public sealed class OrderRepository : IOrderRepository {
        private ITourRepository tourRepository;

        public OrderRepository(ITourRepository tourRepository) {
            this.tourRepository = tourRepository;
        }
      
        public List<Order> getAll() {
            List<Order> orders;
            using (var dbContext = new TourAgencyDbContext()) {
                orders = dbContext.orders
                                  .Include(o => o.user)
                                  .Include(o => o.tour)
                                  .ToList();
            }
            return orders;
        }
        
        public Order findById(long id) {
            Order order;
            using (var dbContext = new TourAgencyDbContext()) {
                order = dbContext.orders
                                 .Include(o => o.user)
                                 .Include(o => o.tour)
                                 .ToList()
                                 .Find(o => o.orderId == id);
            }
            return order;
        }
        
        public Order create(Order newOrder) {
            EntityEntry<Order> order;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.users.Attach(newOrder.user);
                dbContext.tours.Attach(newOrder.tour);
                order = dbContext.orders.Add(newOrder);
                dbContext.SaveChanges();
            }
            return order.Entity;
        }

        public Order update(Order order) {
            EntityEntry<Order> updatedOrder;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.users.Attach(order.user);
                dbContext.tours.Attach(order.tour);
                updatedOrder = dbContext.orders.Update(order);
                dbContext.SaveChanges();
            }
            return updatedOrder.Entity;
        }

        public Order delete(Order order) {
            EntityEntry<Order> removedOrder;
            using (var dbContext = new TourAgencyDbContext()) {
                dbContext.users.Attach(order.user);
                dbContext.tours.Attach(order.tour);
                removedOrder = dbContext.orders.Remove(order);
                dbContext.SaveChanges();
            }
            return removedOrder.Entity;
        }

        public List<Order> findByUserEmail(string email) {
            List<Order> orders;
            using (var dbContext = new TourAgencyDbContext()) {
                orders = dbContext.orders
                                  .Include(o => o.user)
                                  .Include(o => o.tour)
                                  .ToList()
                                  .FindAll(o => o.user.email.Equals(email));
            }
            return orders;
        }

        public List<Order> findByAgentEmail(string email) {
            List<Order> orders = getAll();
            orders?.ForEach(o => {
                o.tour = tourRepository.findById((long) o.tour.tourId);
            });
            orders?.FindAll(o => o.tour.user.email.Equals(email));
            return orders;
        }
    }
}
