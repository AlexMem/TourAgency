using System;
using System.Collections.Generic;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class OrderService {
        private readonly OrderRepository orderRepository;

        public OrderService(OrderRepository orderRepository) {
            this.orderRepository = orderRepository;
        }

        public List<Order> getAll() {
            return orderRepository.getAll();
        }

        public Order get(object[] keyValues) {
            Order order = orderRepository.get(keyValues);
            if (order == null) {
                //TODO
                throw new Exception("Order not found");
            }

            return order;
        }

        public Order create(Order newOrder) {
            verify(newOrder);
            return orderRepository.create(newOrder);
        }

        public Order update(Order order) {
            verify(order);
            return orderRepository.update(order);
        }

        public Order delete(Order order) {
            return orderRepository.delete(order);
        }

        public void verify(Order order) {
            //TODO verification
            throw new NotImplementedException();
        }
    }
}