using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    [Table("offers")]
    public class Offer
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public double square { get; set; }
        public OfferType offertype { get; set; }
        public BuyType buytype { get; set; }
        public int? userid { get; set; }

        public enum OfferType
        {
            Room, 
            House,
            Garage,
            Terrain
        }

        public enum BuyType
        {
            Buy,
            Sell,
            Rent
        }
    }
}
