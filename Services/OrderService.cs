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
        
        public Order create(Order newOrder) {
            verify(newOrder);
//            newOrder.orderDate = DateTime.Now.ToBinary();
            newOrder.finalPrice = countPrice(newOrder);
            return orderRepository.create(newOrder);
        }

        public Order update(Order order) {
            verify(order);
            return orderRepository.update(order);
        }

        public Order delete(Order order) {
            Tour orderTour = order.tour;
            orderTour.maxAmountOfPeople += order.amountOfPeople;
            if (orderTour.maxAmountOfPeople > 0) {
                orderTour.isActive = true;
            }

            tourService.update(orderTour);
            return orderRepository.delete(order);
        }

        public void verify(Order order) {
            if (order.amountOfPeople < 0 || order.amountOfPeople > order.tour.maxAmountOfPeople) {
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