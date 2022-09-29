using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignments09_2.Domain.Services
{
    public class MusicShopService : IMusicShopService
    {
        private readonly IConsoleService _console;
        private readonly IChinookRepository _repository;

        public MusicShopService(IConsoleService console, IChinookRepository repository)
        {
            _console = console;
            _repository = repository;
        }

        public void ManageMusicShop()
        {
            while(true)
            {
                _console.PrintMainMenu();
                var input = _console.GetNumber();
                if (input == 1)
                {
                    _console.ClearConsole();
                    _console.PrintString("Pasirinkite vartotoją:");
                    _console.PrintCustomers(_repository.GetCustomers());
                    var customerId = _console.GetNumber();
                    _console.ClearConsole();
                    _console.PrintPurchaseMenu();
                    var tracks = _repository.GetActiveTracks();
                    continue;
                }
                if(input == 2)
                {
                    RegisterNewCustomer();
                }
            }
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
