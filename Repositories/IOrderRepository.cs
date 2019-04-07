using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Repositories {
    public interface IOrderRepository {
        List<Order> getAll();
        Order findById(long id);
        List<Order> findByUserEmail(string email);
        List<Order> findByAgentEmail(string email);
        Order create(Order newOrder);
        Order update(Order order);
        Order delete(Order order);
    }
}