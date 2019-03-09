using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class Role {
        [Key][Column("id_user_role")]
        public int roleId { get; set; }
        
        public string name { get; set; }
    }
}
