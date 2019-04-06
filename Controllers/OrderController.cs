using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAgency.Models;
using TourAgency.Services;

namespace TourAgency.Controllers {
    [Route("/orders")][Authorize(Roles = "customer, agent, admin")]
    public class OrderController : Controller {
        private OrderService orderService;

        public OrderController(OrderService orderService) {
            this.orderService = orderService;
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

            return View(orders);
        }

        [HttpPost][Authorize(Roles = "customer")]
        public IActionResult createOrder(Order order) {
            try {
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