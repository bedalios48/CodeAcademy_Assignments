using Assignments09_2.Domain.Enums;
using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Models
{
    public class Session
    {
        public Session(decimal taxRate)
        {
            TaxRate = taxRate;
            LoggedIn = false;
            MenuChoice = "0";
            TracksInBasket = new List<Track>();
            PurchasedTracks = new List<Track>();
        }
        public bool LoggedIn { get; set; }
        public Customer Customer { get; set; }
        public string MenuChoice { get; set; }
        public SessionScreen SessionScreen { get; set; }
        public List<Track> TracksInBasket { get; set; }
        public List<Track> PurchasedTracks { get; set; }
        public decimal PriceNoTax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Tax => decimal.Multiply(PriceNoTax, TaxRate);
    }
}
