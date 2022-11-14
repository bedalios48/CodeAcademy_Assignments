using Assignments09_2.Domain.Models;
using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Interfaces
{
    public interface IMusicShopServiceOperations
    {
        void ChangeSongStatus();
        void ChangeCustomerData();
        void ChangeCustomerParameters();
        void ReviewPurchaseHistory(Session session);
        void ReviewBasket(Session session);
        void AddToBasket(List<Track> tracks, Session session);
        void TrackMenu(List<Track> tracks);
        bool DataOptions(List<Track> tracks);
        void SearchTracks(List<Track> tracks);
        void OrderTracks(List<Track> tracks);
    }
}