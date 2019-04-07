using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("/orders")][Authorize(Roles = "customer, agent, admin")]
    public class OrderController : Controller {
        private IOrderService orderService;
        private ITourService tourService;
        private IUserService userService;

        public OrderController(IOrderService orderService,
                               ITourService tourService,
                               IUserService userService) {
            this.orderService = orderService;
            this.tourService = tourService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult orders() {
            List<Order> orders = null;
            if (User.IsInRole("customer")) {
                orders = orderService.findByUserEmail(User.Identity.Name);
            }

            if (User.IsInRole("agent")) {
                orders = orderService.findByAgentEmail(User.Identity.Name);
            }

            if (User.IsInRole("admin")) {
                orders = orderService.getAll();
            }

            orders?.ForEach(o => o.tour = tourService.findById((long) o.tour.tourId));
            return View(orders);
        }


        [HttpGet("/createOrder")][Authorize(Roles = "customer")]
        public IActionResult createOrder(long tourId) {
            Order order = new Order {
                tour = tourService.findById(tourId),
                user = userService.findByEmail(User.Identity.Name)
            };
            return View(order);
        }

        [HttpPost("/createOrder")][Authorize(Roles = "customer")]
        public IActionResult createOrder(Order order, long tourId, long userId) {
            try {
                order.tour = tourService.findById(tourId);
                order.user = userService.findById(userId);
                orderService.create(order);
                ViewData.Add("message", "Order done");
            } catch (Exception e) {
                ViewData.Add("exception", e.Message);
            }

            return Redirect("/orders");
        }

        [HttpPost("/orders/delete")][Authorize(Roles = "customer, admin")]
        public IActionResult deleteOrder(long orderId) {
            try {
                orderService.delete(orderService.findById(orderId));
                ViewData.Add("message", "Order rejected");
            } catch (Exception e) {
                ViewData.Add("exception", e.Message);
            }

            return Redirect("/orders");
        }
    }
}