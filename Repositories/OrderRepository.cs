using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TourAgency.Contexts;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public sealed class OrderRepository {
      
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
    }
}