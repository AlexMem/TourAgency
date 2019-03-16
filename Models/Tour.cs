using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TourAgency.Repositories;

namespace TourAgency.Models {
    public class Tour {
        [Key][Column("id_tour")]
        public long? tourId { get; set; }

        [Column("id_user")] private long? _userId;
        [ForeignKey("userId")] private User _user;
        public User user {
            get => _user;
            set {
                _userId = value.userId;
                _user = value;
            }
        }

        [Column("id_tour_type")] private long? _typeId;
        [ForeignKey("typeId")] private Type _type;
        public Type type {
            get => _type;
            set {
                _typeId = value.typeId;
                _type = value;
            }
        }
        
        [Column("is_active")]
        public bool isActive { get; set; }
        
        [Timestamp][Column("from_date")]
        public long fromDate { get; set; }
        
        public int duration { get; set; }
        
        [Column("is_hot")]
        public bool isHot { get; set; }
        
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