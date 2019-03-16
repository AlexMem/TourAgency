using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class User {
        [Key][Column("id_user")]
        public long? userId { get; set; }
        
        public string password { get; set; }

        [Column("id_user_role")] private long? _roleId;
        [ForeignKey("roleId")] private Role _role;
        public Role role {
            get => _role;
            set {
                _roleId = value.roleId;
                _role = value;
            }
        }
        
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