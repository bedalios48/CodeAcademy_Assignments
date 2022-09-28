using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;
using Assignments09_2.Infrastructure.Database;

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
    }
}
