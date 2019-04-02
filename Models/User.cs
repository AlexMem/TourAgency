using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class User {
        [Key]
        public long? userId { get; set; }
        
        public string password { get; set; }

        public virtual Role role { get; set; }
        
        public bool isRegular { get; set; }
        
        public string email { get; set; }
        
        public string firstName { get; set; }
        
        public string lastName { get; set; }
        
        public string telephone { get; set; }
    }
}