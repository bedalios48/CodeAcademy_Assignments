using Assignments09_2.Domain.Models;

namespace Assignments09_2.Domain.Interfaces
{
    public interface IChinookRepository
    {
        List<Customer> GetCustomers();
        void AddNewCustomer(Customer customer);
        List<Track> GetActiveTracks();
    }
}