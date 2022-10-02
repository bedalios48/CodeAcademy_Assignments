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
        private readonly IMusicShopServiceOperations _operations;

        public MusicShopService(IConsoleService console,
            IChinookRepository repository,
            IMusicShopServiceOperations operations)
        {
            _console = console;
            _repository = repository;
            _session = new Session((decimal)0.21);
            _operations = operations;
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
                    case 3:
                        EmployeeSettings();
                        break;
                }
            }
        }

        private void EmployeeSettings()
        {
            while(true)
            {
                _console.ClearConsole();
                _console.PrintEmployeeSettingsMenu();
                var input = _console.GetNumber(new int[] { 1, 2, 3, 4 });
                switch (input)
                {
                    case 1:
                        _operations.ChangeCustomerData();
                        break;
                    case 2:
                        _operations.ChangeSongStatus();
                        break;
                    case 4:
                        return;
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
                var menuChoice = _console.GetNumber(new int[] {1, 2, 3, 4, 5});
                var tracks = _repository.GetActiveTracks();
                switch (menuChoice)
                {
                    case 1:
                        _operations.TrackMenu(tracks);
                        break;
                    case 2:
                        _operations.AddToBasket(tracks, _session);
                        break;
                    case 3:
                        _operations.ReviewBasket(_session);
                        break;
                    case 4:
                        _operations.ReviewPurchaseHistory(_session);
                        break;
                    case 5:
                        _session = new Session((decimal)0.21);
                        return;
                };
            }
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
            try
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
                    var t = typeof(KeyAttribute);
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
                _repository.AddNewCustomer(customer);
            }
            catch (Exception e)
            {
                _console.PrintString("Registracija nepavyko!" + Environment.NewLine + e.Message);
                _console.PrintString("'q' - Grįžti atgal");
                _console.GetInput(new string[] { "q" });
            }
            var lastCustomer = _repository.GetCustomers().LastOrDefault();
            _console.PrintString($"Registracija sekminga. Naujas vartotojas yra:" +
                $" {lastCustomer.CustomerId}. {lastCustomer.FirstName} {lastCustomer.LastName}");
            _console.PrintString("'q' - Grįžti atgal");
            _console.GetInput(new string[] { "q" });
        }
    }
}
