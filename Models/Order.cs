using System;

namespace TourAgency.Models {
    public class Order {
        public long? orderId { get; set; }

        public User user { get; set; }

        public Tour tour { get; set; }
        
        public DateTime orderDate { get; set; }
        
        public int amountOfPeople { get; set; }
        
        public double finalPrice { get; set; }
    }
}