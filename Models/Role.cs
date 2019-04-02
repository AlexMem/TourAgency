using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourAgency.Models {
    public class Role {
        public long? roleId { get; set; }
        public string name { get; set; }

        public virtual List<User> users { get; set; }
    }
}
