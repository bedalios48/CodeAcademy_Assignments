using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Models
{
    public class Session
    {
        public Session(decimal taxRate)
        {
            TaxRate = taxRate;
            TracksInBasket = new List<Track>();
            PurchasedTracks = new List<Track>();
        }
        public Customer Customer { get; set; }
        public List<Track> TracksInBasket { get; set; }
        public List<Track> PurchasedTracks { get; set; }
        public decimal PriceNoTax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Tax => decimal.Multiply(PriceNoTax, TaxRate);
    }
}
