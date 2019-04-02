using System.Collections.Generic;

namespace TourAgency.Models {
    public class Type {
        public long? typeId { get; set; }
        public string name { get; set; }
        
        public virtual List<Tour> tours { get; set; }
    }
}