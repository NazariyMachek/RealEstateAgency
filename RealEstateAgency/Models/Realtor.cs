using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    [Table("realtors")]
    public class Realtor
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public int experience { get; set; }
        public int? agencyid { get; set; }
    }
}
