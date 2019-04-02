using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TourAgency.Repositories;

namespace TourAgency.Models {
    public class Tour {
        public long? tourId { get; set; }

        public User user { get; set; }

        public Type type { get; set; }
        
        public bool isActive { get; set; }
        
        public DateTime fromDate { get; set; }
        
        public int duration { get; set; }
        
        public bool isHot { get; set; }
        
        public double hotDiscount { get; set; }
        
        public double discount { get; set; }
        
        public double price { get; set; }
        
        public int maxAmountOfPeople { get; set; }
        
        public string place { get; set; }
        
        public string description { get; set; }
    }
}