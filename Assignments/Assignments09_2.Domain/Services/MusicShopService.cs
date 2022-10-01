using Assignments09_2.Domain.Enums;
using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;
using Assignments09_2.Domain.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace Assignments09_2.Domain.Services
{
    public class MusicShopService : IMusicShopService
    {
        private readonly IConsoleService _console;
        private readonly IChinookRepository _repository;
        private Session _session;

        public MusicShopService(IConsoleService console, IChinookRepository repository)
        {
            _console = console;
            _repository = repository;
            _session = new Session((decimal)0.21);
        }

        public void ManageMusicShop()
        {
            while(true)
            {
                var input = MainMenu();
                switch (input)
                {
                    case 1:
                        _session.Customer = _repository.GetCustomer(long.Parse(Login()));
                        PurchaseMenu();
                        break;
                    case 2:
                        RegisterNewCustomer();
                        break;
                }
            }
        }

        private void PurchaseMenu()
        {
            while (true)
            {
                _console.ClearConsole();
                _console.PrintString("Pasirinkite meniu punktą:");
                _console.PrintPurchaseMenu();
                var menuChoice = _console.GetNumber();
                var tracks = _repository.GetActiveTracks();
                switch (menuChoice)
                {
                    case 1:
                        TrackMenu(tracks);
                        break;
                    case 2:
                        AddToBasket(tracks);
                        break;
                    case 3:
                        ReviewBasket();
                        break;
                };
            }
        }

        private void ReviewBasket()
        {
            _console.PrintTrackMenu(_session.TracksInBasket);
            _console.PrintString("'q' - Grįžti atgal || 'y' - Užbaigti pirkimą");
            var option = _console.GetInput(new string[] { "y", "q" });
            switch(option)
            {
                case "y":
                    FinishPurchase();
                    break;
            }
        }

        private void AddToBasket(List<Track> tracks)
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
                    _session.TracksInBasket.AddRange(tracksToBasket);
                    break;
            }
        }

        private void FinishPurchase()
        {
            _session.PurchasedTracks.AddRange(_session.TracksInBasket);
            _session.TracksInBasket.Clear();
            _session.PriceNoTax = _session.PurchasedTracks.Sum(t => t.Price);
            var invoice = new Invoice(_session, DateTime.Now);
            _repository.AddNewInvoice(invoice);
            _console.PrintInvoiceInfo(_session);
            _console.PrintString("'q' - Grįžti atgal");
            _console.GetInput(new string[] { "q" });
        }

        private void TrackMenu(List<Track> tracks)
        {
            _console.ClearConsole();
            _console.PrintTrackMenu(tracks);
            while (DataOptions(tracks));
        }

        private bool DataOptions(List<Track> tracks)
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

        private void SearchTracks(List<Track> tracks)
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
                    switch(dm)
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

        private void OrderTracks(List<Track> tracks)
        {
            _console.ClearConsole();
            _console.PrintOrderOptions();
            var menuChoice = _console.GetNumber();
            switch(menuChoice)
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

        private int MainMenu()
        {
            _console.ClearConsole();
            _console.PrintMainMenu();
            return _console.GetNumber();
        }

        private string Login()
        {
            _console.ClearConsole();
            _console.PrintString("Pasirinkite vartotoją:");
            _console.PrintCustomers(_repository.GetCustomers());
            return _console.GetNumber().ToString();
        }

        private void RegisterNewCustomer()
        {
            _console.ClearConsole();
            var customer = new Customer();
            var customerProperties = customer.GetType().GetProperties();
            foreach (var property in customerProperties)
            {
                var required = false;
                _console.PrintString(property.Name);
                var type = property.PropertyType;
                var attributes = property.GetCustomAttributes(true);
                var t = typeof(System.ComponentModel.DataAnnotations.KeyAttribute);
                if (attributes.Any(a => a.GetType() == t))
                    continue;
                var requiredType = typeof(RequiredAttribute);
                if (attributes.Any(a => a.GetType() == requiredType))
                    required = true;
                if (Nullable.GetUnderlyingType(type) != null)
                    type = Nullable.GetUnderlyingType(type);
                _console.PrintString(type.Name);
                var value = _console.GetValue();
                if (required)
                {
                    while (string.IsNullOrEmpty(value))
                    {
                        _console.PrintString("Si verte privaloma!");
                        value = _console.GetValue();
                    }
                }
                if (!string.IsNullOrEmpty(value))
                {
                    var a = Convert.ChangeType(value, type);
                    property.SetValue(customer, a);
                }
            }
            try
            {
                _repository.AddNewCustomer(customer);
            }
            catch (Exception e)
            {
                _console.PrintString("Registracija nepavyko!" + Environment.NewLine + e.Message);
            }
            var lastCustomer = _repository.GetCustomers().LastOrDefault();
            _console.PrintString($"Registracija sekminga. Naujas vartotojas yra:" +
                $" {lastCustomer.CustomerId}. {lastCustomer.FirstName} {lastCustomer.LastName}");
        }
    }
}
