using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models.Database;
using Assignments09_2.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Assignments09_2.Infrastructure.Repositories
{
    public class ChinookRepository : IChinookRepository
    {
        private readonly chinookContext _context;
        public ChinookRepository(chinookContext context)
        {
            _context = context;
        }

        public List<Customer> GetCustomers() => _context.Customers.ToList();

        public void AddNewCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public List<Track> GetActiveTracks() => _context.Tracks.Where(t => t.Status == "Active")
            .Include(x => x.Genre)
            .Include(x => x.Album).ToList();

        public string GetGenreName(long? genreId) => genreId == null ? "" :
            _context.Genres.Where(g => g.GenreId == genreId).FirstOrDefault().Name;

        public string GetAlbumTitle(long? albumId) => albumId == null ? "" :
            _context.Albums.Where(a => a.AlbumId == albumId).FirstOrDefault().Title;

        public Customer GetCustomer(long customerId)
        {
            return _context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();
        }

        public void AddNewInvoice(Invoice invoice)
        {
            _context.Add(invoice);
            _context.SaveChanges();
        }
    }
}
