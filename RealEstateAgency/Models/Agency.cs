using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    [Table("agencies")]
    public class Agency
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
