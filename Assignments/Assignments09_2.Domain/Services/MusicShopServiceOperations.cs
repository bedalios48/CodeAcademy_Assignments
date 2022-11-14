using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;
using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Services
{
    public class MusicShopServiceOperations : IMusicShopServiceOperations
    {
        private readonly IConsoleService _console;
        private readonly IChinookRepository _repository;

        public MusicShopServiceOperations(IConsoleService console,
            IChinookRepository repository)
        {
            _console = console;
            _repository = repository;
        }
        public void ChangeSongStatus()
        {
            while (true)
            {
                _console.ClearConsole();
                _console.PrintChangeSongStatusMenu();
                var input = _console.GetNumber(new int[] { 1, 2, 3 });
                switch (input)
                {
                    case 1:
                        _console.PrintTracks(_repository.GetTracks());
                        _console.PrintString("'q' - Grįžti atgal");
                        _console.GetInput(new string[] { "q" });
                        break;
                    case 2:
                        _console.PrintString("Įveskite dainos Id:");
                        var trackId = _console.GetNumber();
                        var track = _repository.GetTrack(trackId);
                        _console.PrintString($"Dainos statusas yra {track.Status}. Ar norite pakeisti? t/n");
                        var choice = _console.GetInput(new string[] { "t", "n" });
                        if (choice == "t")
                        {
                            if (track.Status == "Active")
                                track.Status = "Inactive";
                            else
                                track.Status = "Active";
                            _console.PrintString("Statusas pakeistas");
                        }
                        _repository.UpdateTrack(track);
                        _console.PrintString("'q' - Grįžti atgal");
                        _console.GetInput(new string[] { "q" });
                        break;
                    case 3:
                        return;
                }
            }
        }

        public void ChangeCustomerData()
        {
            while (true)
            {
                _console.ClearConsole();
                _console.PrintChangeCustomerDataMenu();
                var input = _console.GetNumber(new int[] { 1, 2, 3, 4 });
                switch (input)
                {
                    case 1:
                        _console.PrintCustomers(_repository.GetCustomers());
                        _console.PrintString("'q' - Grįžti atgal");
                        _console.GetInput(new string[] { "q" });
                        break;
                    case 2:
                        _console.PrintString("Įveskite kliento ID:");
                        var id = _console.GetNumber();
                        _repository.DeleteCustomer(id);
                        _console.PrintString("Klientas ištrintas. 'q' - Grįžti atgal");
                        _console.GetInput(new string[] { "q" });
                        break;
                    case 3:
                        ChangeCustomerParameters();
                        break;
                    case 4:
                        return;
                }
            }
        }

        public void ChangeCustomerParameters()
        {
            _console.PrintString("Įveskite kliento ID:");
            var id = _console.GetNumber();
            var customer = _repository.GetCustomer(id);
            var customerProperties = customer.GetType().GetProperties().Where(p => !p.GetMethod.IsVirtual);
            foreach (var property in customerProperties)
            {
                var value = property.GetValue(customer);
                var valueString = value == null ? "" : value.ToString();
                _console.PrintString($"{property.Name}: {valueString}");
            }
            _console.PrintString("Įrašykite, kurį parametrą norite keisti:");
            var propertyName = _console.GetValue();
            var propertyToChange = customer.GetType().GetProperty(propertyName);
            _console.PrintString("Įrašykite naują vertę:");
            var propertyValue = _console.GetValue();
            var type = propertyToChange.PropertyType;
            if (Nullable.GetUnderlyingType(type) != null)
                type = Nullable.GetUnderlyingType(type);
            var a = Convert.ChangeType(propertyValue, type);
            propertyToChange.SetValue(customer, a);
            _repository.UpdateCustomer(customer);
            _console.PrintString("Klientas modifikuotas. 'q' - Grįžti atgal");
            _console.GetInput(new string[] { "q" });
        }

        public void ReviewPurchaseHistory(Session session)
        {
            var invoices = _repository.GetAllInvoices(session.Customer.CustomerId);
            _console.PrintCustomerPurchases(invoices, session.Customer);
            _console.PrintString("'q' - Grįžti atgal");
            _console.GetInput(new string[] { "q" });
        }

        public void ReviewBasket(Session session)
        {
            _console.PrintTrackMenu(session.TracksInBasket);
            _console.PrintString("'q' - Grįžti atgal || 'y' - Užbaigti pirkimą");
            var option = _console.GetInput(new string[] { "y", "q" });
            switch (option)
            {
                case "y":
                    FinishPurchase(session);
                    break;
            }
        }

        public void AddToBasket(List<Track> tracks, Session session)
        {
            _console.ClearConsole();
            _console.PrintAddToBasketOptions();
            var menuChoice = _console.GetNumber();
            _console.PrintString("Įveskite paieškos lauką:");
            var value = _console.GetValue();
            var tracksToBasket = new List<Track>();
            switch (menuChoice)
            {
                case 1:
                    tracksToBasket = tracks.Where(t => t.TrackId == long.Parse(value)).ToList();
                    break;
                case 2:
                    tracksToBasket = tracks.Where(t => t.Name == value).ToList();
                    break;
                case 3:
                    tracksToBasket = tracks.Where(t => t.AlbumId == long.Parse(value)).ToList();
                    break;
                case 4:
                    tracksToBasket = tracks.Where(t => t.Album.Title == value).ToList();
                    break;
            }
            _console.PrintTrackMenu(tracksToBasket);
            _console.PrintString("Pridėti pasirinkimus į krepšelį ('y'); Grįžti atgal ('q')");
            var option = _console.GetInput(new string[] { "y", "q" });
            switch (option)
            {
                case "y":
                    session.TracksInBasket.AddRange(tracksToBasket);
                    break;
            }
        }

        private void FinishPurchase(Session session)
        {
            session.PurchasedTracks.AddRange(session.TracksInBasket);
            session.TracksInBasket.Clear();
            session.PriceNoTax = session.PurchasedTracks.Sum(t => t.Price);
            var invoice = new Invoice(session, DateTime.Now);
            _repository.AddNewInvoice(invoice);
            _console.PrintInvoiceInfo(session);
            session.PurchasedTracks.Clear();
            session.PriceNoTax = 0;
            _console.PrintString("'q' - Grįžti atgal");
            _console.GetInput(new string[] { "q" });
        }

        public void TrackMenu(List<Track> tracks)
        {
            _console.ClearConsole();
            _console.PrintTrackMenu(tracks);
            while (DataOptions(tracks)) ;
        }

        public bool DataOptions(List<Track> tracks)
        {
            _console.PrintDataOptions();
            var menuChoice = _console.GetInput(new string[] { "q", "o", "s" });
            if (menuChoice == "q")
                return false;
            if (menuChoice == "o")
                OrderTracks(tracks);
            if (menuChoice == "s")
                SearchTracks(tracks);
            return true;
        }

        public void SearchTracks(List<Track> tracks)
        {
            _console.ClearConsole();
            _console.PrintSearchOptions();
            var menuChoice = _console.GetNumber();
            _console.PrintString("Įveskite paieškos lauką:");
            var value = _console.GetValue();
            var searchTracks = new List<Track>();
            switch (menuChoice)
            {
                case 1:
                    searchTracks = tracks.Where(t => t.TrackId == long.Parse(value)).ToList();
                    break;
                case 2:
                    searchTracks = tracks.Where(t => t.Name == value).ToList();
                    break;
                case 3:
                    searchTracks = tracks.Where(t => t.Composer == value).ToList();
                    break;
                case 4:
                    searchTracks = tracks.Where(t => t.Genre.Name == value).ToList();
                    break;
                case 5:
                    var value2 = _console.GetValue();
                    searchTracks = tracks.Where(t => t.Composer == value).Where(t => t.Album.Title == value2).ToList();
                    break;
                case 6:
                    _console.PrintString("Daugiau ar mažiau? d/m");
                    var dm = _console.GetInput(new string[] { "d", "m" });
                    switch (dm)
                    {
                        case "d":
                            searchTracks = tracks.Where(t => t.Milliseconds > long.Parse(value)).ToList();
                            break;
                        case "m":
                            searchTracks = tracks.Where(t => t.Milliseconds < long.Parse(value)).ToList();
                            break;
                    }
                    break;
            }
            _console.PrintTrackMenu(searchTracks);
        }

        public void OrderTracks(List<Track> tracks)
        {
            _console.ClearConsole();
            _console.PrintOrderOptions();
            var menuChoice = _console.GetNumber();
            switch (menuChoice)
            {
                case 1:
                    tracks = tracks.OrderBy(t => t.Name).ToList();
                    break;
                case 2:
                    tracks = tracks.OrderByDescending(t => t.Name).ToList();
                    break;
                case 3:
                    tracks = tracks.OrderBy(t => t.Composer).ToList();
                    break;
                case 4:
                    tracks = tracks.OrderBy(t => t.Genre.Name).ToList();
                    break;
                case 5:
                    tracks = tracks.OrderBy(t => t.Composer).OrderBy(t => t.Album.Title).ToList();
                    break;
            }
            _console.ClearConsole();
            _console.PrintTrackMenu(tracks);
        }
    }
}
