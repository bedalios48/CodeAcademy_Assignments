using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;
using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly IChinookRepository _repository;
        public ConsoleService()
        {
                //_repository = repository;
        }
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

        public void PrintTrackMenu(List<Track> tracks)
        {
            foreach(var track in tracks)
            {
                Console.WriteLine($"{track.TrackId}. {track.Name} {track.Composer} {track.Genre.Name} " +
                    $"{track.Album.Title} {track.Milliseconds} {track.Price}");
            }
        }

        public void PrintDataOptions()
        {
            Console.WriteLine("'q' - grįžti atgal");
            Console.WriteLine("'o' - rikiuoti įrašus");
            Console.WriteLine("'s' - paieškos langas");
        }

        public string GetInput(string[] allowedValues)
        {
            var input = Console.ReadLine();
            while(!allowedValues.Contains(input.ToLower()))
            {
                Console.WriteLine("Neteisinga įvestis!");
                input = Console.ReadLine();
            }
            return input;
        }

        public void PrintOrderOptions()
        {
            Console.WriteLine("Pasirinkite, pagal ką rikiuoti:");
            Console.WriteLine("1. Pagal Name abecėlės tvarka");
            Console.WriteLine("2. Pagal Name atvirkštine abecėlės tvarka");
            Console.WriteLine("3. Pagal Composer");
            Console.WriteLine("4. Pagal Genre");
            Console.WriteLine("5. Pagal Composer ir Album");
        }

        public void PrintSearchOptions()
        {
            Console.WriteLine("Pasirinkite, pagal ką ieškoti:");
            Console.WriteLine("1. Pagal Id");
            Console.WriteLine("2. Pagal Name");
            Console.WriteLine("3. Pagal Composer");
            Console.WriteLine("4. Pagal Genre");
            Console.WriteLine("5. Pagal Composer ir Album");
            Console.WriteLine("6. Pagal Milliseconds (Mažiau nei X arba daugiau nei X)");
        }

        public void PrintAddToBasketOptions()
        {
            Console.WriteLine("Pasirinkite, pagal ką įdėti į krepšelį:");
            Console.WriteLine("1. Daina pagal Id");
            Console.WriteLine("2. Daina pagal pavadinimą");
            Console.WriteLine("3. Dainos pagal albumo Id");
            Console.WriteLine("4. Dainos pagal albumo pavadinimą");
        }

        public void PrintInvoiceInfo(Session session)
        {
            Console.WriteLine($"Name:{session.Customer.FirstName}");
            Console.WriteLine($"Surname:{session.Customer.LastName}");
            Console.WriteLine($"Phone:{session.Customer.Phone}");
            Console.WriteLine($"Address:{session.Customer.Address}");
            Console.WriteLine($"Pašto kodas:{session.Customer.PostalCode}");
            PrintTrackMenu(session.PurchasedTracks);
            Console.WriteLine($"Visa suma be mokesčių: {session.PriceNoTax}");
            Console.WriteLine($"Mokesčiai: {session.Tax:#.##}");
            Console.WriteLine($"Iš viso: {session.PriceNoTax + session.Tax:#.##}");
        }
    }
}
