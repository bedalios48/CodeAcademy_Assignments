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

        public void PrintString(string value) => Console.WriteLine(value);
        public string GetValue() => Console.ReadLine();
        public void ClearConsole() => Console.Clear();

        public void PrintPurchaseMenu()
        {
            var menu = "-------------------------------------------------------------- " + Environment.NewLine +
            "| #       | Pasirinkimas | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 1.  | Peržiūrėti katalogą | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 2.  | Įdėti į krepšelį     | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 3.  | Peržiūrėti krepšelį | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 4.  | Peržiūrėti pirkimų istorija(Išrašai) | " + Environment.NewLine +
            "--------------------------------------------------------------";
            Console.WriteLine(menu);
        }
    }
}
