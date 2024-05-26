using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    [Table("reviews")]
    public class Review
    {
        public int id { get; set; }
        public string review { get; set; }
        public int? offerid { get; set; }
        public int? userid { get; set; }
    }
}
