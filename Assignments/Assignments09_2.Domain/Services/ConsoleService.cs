using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;

namespace Assignments09_2.Domain.Services
{
    public class ConsoleService : IConsoleService
    {
        public void PrintMainMenu()
        {
            var mainMenu = "-------------------------------------------------------------- " + Environment.NewLine +
            "| #       | Pasirinkimas | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 1.  | Prisijungti | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 2.  | Registruotis | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 3.  | Darbuotojų Parinktys | " + Environment.NewLine +
            "--------------------------------------------------------------";
            Console.WriteLine(mainMenu);
        }

        public int GetNumber()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
                Console.WriteLine("Neteisinga ivestis!");

            return input;
        }

        public void PrintCustomers(List<Customer> customers)
        {
            foreach(var customer in customers)
            {
                Console.WriteLine(customer.CustomerId + ". " + customer.FirstName + " " + customer.LastName);
            }
        }
    }
}
