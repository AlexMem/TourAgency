using System.Collections.Generic;
using TourAgency.Models;

namespace TourAgency.Services {
    public interface IOrderService {
        List<Order> getAll();
        Order findById(long id);
        List<Order> findByUserEmail(string email);
        List<Order> findByAgentEmail(string email);
        Order create(Order newOrder);
        Order delete(Order order);
        void verify(Order order);
    }
}