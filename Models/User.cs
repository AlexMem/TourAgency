using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class User {
        [Key][Column("id_user")]
        public int userId { get; set; }
        
        public string password { get; set; }
        
        [Column("id_role")]
        public int roleId { get; set; }
        [ForeignKey("roleId")]
        public Role role { get; set; }
        
        [Column("is_regular")]
        public bool isRegular { get; set; }
        
        public string email { get; set; }
        
        [Column("first_name")]
        public string firstName { get; set; }
        
        [Column("last_name")]
        public string lastName { get; set; }
        
        public string telephone { get; set; }
    }
}