using System;
using System.Collections.Generic;
using System.Text;

namespace Assignments09_2.Domain.Models.Database
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public Invoice(Session session, DateTime date)
        {
            CustomerId = session.Customer.CustomerId;
            InvoiceDate = Encoding.ASCII.GetBytes(date.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            BillingAddress = session.Customer.Address;
            BillingCity = session.Customer.City;
            BillingState = session.Customer.State;
            BillingCountry = session.Customer.Country;
            BillingPostalCode = session.Customer.PostalCode;
            Total = Encoding.ASCII.GetBytes((session.PriceNoTax + session.Tax).ToString("#.##"));
            InvoiceItems = session.PurchasedTracks.GroupBy(t => t.TrackId).Select(t => new InvoiceItem
            {
                InvoiceId = InvoiceId,
                TrackId = t.First().TrackId,
                UnitPrice = t.First().UnitPrice,
                Quantity = t.Count(),
                Track = t.First()
            }).ToList();
        }

        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        public byte[] InvoiceDate { get; set; } = null!;
        public string? BillingAddress { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingCountry { get; set; }
        public string? BillingPostalCode { get; set; }
        public byte[] Total { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual decimal TotalPrice => decimal.Parse(new string(Total.Select(x => Convert.ToChar(x)).ToArray()));
    }
}
