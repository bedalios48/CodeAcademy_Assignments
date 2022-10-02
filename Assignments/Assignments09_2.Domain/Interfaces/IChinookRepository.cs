using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Interfaces
{
    public interface IChinookRepository
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(long customerId);
        void AddNewCustomer(Customer customer);
        List<Track> GetActiveTracks();
        string GetGenreName(long? genreId);
        string GetAlbumTitle(long? albumId);
        void AddNewInvoice(Invoice invoice);
        List<Invoice> GetAllInvoices(long customerId);
        void DeleteCustomer(long customerId);
        void UpdateCustomer(Customer customer);
        List<Track> GetTracks();
        Track GetTrack(int trackId);
        void UpdateTrack(Track track);
    }
}