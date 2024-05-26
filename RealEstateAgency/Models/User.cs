using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    [Table("users")]
    public class User
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Role role { get; set; }

        public enum Role
        {
            Owner, 
            Administrator, 
            Operator,
            Guest
        }
    }
}
