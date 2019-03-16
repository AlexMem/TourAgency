using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class Type {
        [Key][Column("id_tour_type")]
        public long? typeId { get; set; }
        
        public string name { get; set; }
    }
}