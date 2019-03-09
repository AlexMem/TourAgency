using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class Tour {
        [Key][Column("id_tour")]
        public int tourId { get; set; }
        
        [Column("id_user")]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public User user { get; set; }
        
        [Column("id_tour_type")]
        public int typeId { get; set; }
        [ForeignKey("typeId")]
        public Type type { get; set; }
        
        [Column("is_active")]
        public bool isActive { get; set; }
        
        [Timestamp][Column("from_date")]
        public byte[] fromDate { get; set; }
        
        public int duration { get; set; }
        
        [Column("is_hot")]
        public int isHot { get; set; }
        
        [Column("hot_discount")]
        public double hotDiscount { get; set; }
        
        public double discount { get; set; }
        
        public double price { get; set; }
        
        [Column("max_amount_of_people")]
        public int maxAmountOfPeople { get; set; }
        
        public string place { get; set; }
        
        public string description { get; set; }
    }
}