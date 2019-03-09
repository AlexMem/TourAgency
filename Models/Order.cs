using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class Order {
        [Key][Column("id_order")]
        public int orderId { get; set; }
        
        [Column("id_user")]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public User user { get; set; }
        
        [Column("id_tour")]
        public int tourId { get; set; }
        [ForeignKey("tourId")]
        public Tour tour { get; set; }
        
        [Timestamp][Column("order_date")]
        public byte[] orderDate { get; set; }
        
        [Column("amount_of_people")]
        public int amountOfPeople { get; set; }
        
        [Column("final_price")]
        public double finalPrice { get; set; }
    }
}