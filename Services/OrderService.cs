using System;
using System.Collections.Generic;
using TourAgency.Models;
using TourAgency.Repositories;

namespace TourAgency.Services {
    public class OrderService {
        private readonly OrderRepository orderRepository;
        private readonly TourService tourService;
        private readonly UserService userService;

        public OrderService(OrderRepository orderRepository,
                            TourService tourService,
                            UserService userService) {
            this.orderRepository = orderRepository;
            this.tourService = tourService;
            this.userService = userService;
        }

        public List<Order> getAll() {
            return orderRepository.getAll();
        }
        
        public Order findById(long id) {
            return orderRepository.findById(id);
        }
        
        public List<Order> findByUserEmail(string email) {
            return orderRepository.findByUserEmail(email);
        }

        public List<Order> findByAgentEmail(string email) {
            return orderRepository.findByAgentEmail(email);
        }
        
        public Order create(Order newOrder) {
            verify(newOrder);
            newOrder.orderDate = DateTime.Now;
            
            Tour orderTour = tourService.findById(newOrder.tour.tourId.Value);
            orderTour.maxAmountOfPeople -= newOrder.amountOfPeople;
            if (orderTour.maxAmountOfPeople == 0) {
                orderTour.isActive = false;
            }

            tourService.update(orderTour);
            return orderRepository.create(newOrder);
        }

        public Order delete(Order order) {
            Tour orderTour = tourService.findById(order.tour.tourId.Value);
            orderTour.maxAmountOfPeople += order.amountOfPeople;
            if (orderTour.maxAmountOfPeople > 0) {
                orderTour.isActive = true;
            }

            tourService.update(orderTour);
            return orderRepository.delete(order);
        }

        public void verify(Order order) {
            Tour actualTour = tourService.findById(order.tour.tourId.Value);
            if (order.amountOfPeople < 0 || order.amountOfPeople > actualTour.maxAmountOfPeople) {
                throw new Exception("Illegal amount of people");
            }
        }

        private double countPrice(Order order) {
            Tour tour = order.tour;
            User user = order.user;
            
            if (user.isRegular && tour.isHot) {
                if (tour.hotDiscount > tour.discount) {
                    return order.amountOfPeople * (tour.price * ((100 - tour.hotDiscount) / 100));
                }
                return order.amountOfPeople * (tour.price * ((100 - tour.discount) / 100));
            }

            if (tour.isHot && !user.isRegular) {
                return order.amountOfPeople * (tour.price * ((100 - tour.hotDiscount) / 100));
            }
            
            if (user.isRegular && !tour.isHot) {
                return order.amountOfPeople * (tour.price * ((100 - tour.discount) / 100));
            }

            return  order.amountOfPeople * tour.price;
        }
    }
}