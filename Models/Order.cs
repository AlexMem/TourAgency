using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Protobuf.WellKnownTypes;

namespace TourAgency.Models {
    public class Order {
        [Key][Column("id_order")]
        public long? orderId { get; set; }

        [Column("id_user")] private long? _userId;
        [ForeignKey("userId")] private User _user;
        public User user {
            get => _user;
            set {
                _userId = value.userId;
                _user = value;
            }
        }

        [Column("id_tour")] private long? _tourId;
        [ForeignKey("tourId")] private Tour _tour;
        public Tour tour {
            get => _tour;
            set {
                _tourId = value.tourId;
                _tour = value;
            }
        }
        
        [Timestamp][Column("order_date")]
        public long orderDate { get; set; }
        
        [Column("amount_of_people")]
        public int amountOfPeople { get; set; }
        
        [Column("final_price")]
        public double finalPrice { get; set; }
    }
}