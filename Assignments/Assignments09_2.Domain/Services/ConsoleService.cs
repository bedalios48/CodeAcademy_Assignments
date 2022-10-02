using Assignments09_2.Domain.Interfaces;
using Assignments09_2.Domain.Models;
using Assignments09_2.Domain.Models.Database;
using System.Text;

namespace Assignments09_2.Domain.Services
{
    public class ConsoleService : IConsoleService
    {
        public ConsoleService()
        {
            Console.OutputEncoding = Encoding.GetEncoding(1200);
            Console.InputEncoding = Encoding.GetEncoding(1200);
        }
        public void PrintMainMenu()
        {
            var mainMenu = "-------------------------------------------------------------- " + Environment.NewLine +
            "| #   | Pasirinkimas         | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 1.  | Prisijungti          | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 2.  | Registruotis         | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 3.  | Darbuotojų Parinktys | " + Environment.NewLine +
            "--------------------------------------------------------------";
            Console.WriteLine(mainMenu);
        }

        public int GetNumber(int[]? allowedNumbers = null)
        {
            int input;
            while (!(int.TryParse(Console.ReadLine(), out input) &&
                (allowedNumbers == null || allowedNumbers.Contains(input))))
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
            "| #   | Pasirinkimas                          | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 1.  | Peržiūrėti katalogą                   | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 2.  | Įdėti į krepšelį                      | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 3.  | Peržiūrėti krepšelį                   | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 4.  | Peržiūrėti pirkimų istoriją (Išrašai) | " + Environment.NewLine +
            "--------------------------------------------------------------" + Environment.NewLine +
            "| 5.  | Atsijungti                            | " + Environment.NewLine +
            "--------------------------------------------------------------";
            Console.WriteLine(menu);
        }

        public void PrintTrackMenu(List<Track> tracks)
        {
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #       |  Pavadinimas, Autorius, Žanras, Albumas, Milisekundės, Kaina | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            foreach (var track in tracks)
            {
                Console.WriteLine($"|{track.TrackId,4}. | {track.Name}, {track.Composer}, {track.Genre.Name}, " +
                    $"{track.Album.Title}, {track.Milliseconds}, {track.Price}");
            }
            Console.WriteLine("-------------------------------------------------------------- ");
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
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |   Pasirinkimas                               | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| 1.  |   Rasti dainą pagal Id                       |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2.  |   Rasti dainą pagal pavadinimą               |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3.  |   Rasti dainas pagal albumo Id               |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 4.  |   Rasti dainas pagal albumo pavadinimą       |  ");
            Console.WriteLine("--------------------------------------------------------------");
        }

        public void PrintInvoiceInfo(Session session)
        {
            PrintCustomerInfo(session.Customer);
            PrintTrackMenu(session.PurchasedTracks);
            Console.WriteLine($"Visa suma be mokesčių: {session.PriceNoTax}");
            Console.WriteLine($"Mokesčiai: {session.Tax:#.##}");
            Console.WriteLine($"Iš viso: {session.PriceNoTax + session.Tax:#.##}");
        }

        private void PrintCustomerInfo(Customer customer)
        {
            Console.WriteLine($"Vardas:{customer.FirstName}");
            Console.WriteLine($"Pavardė:{customer.LastName}");
            Console.WriteLine($"Telefonas:{customer.Phone}");
            Console.WriteLine($"Adresas:{customer.Address}");
            Console.WriteLine($"Pašto kodas:{customer.PostalCode}");
        }

        public void PrintCustomerPurchases(List<Invoice> invoices, Customer customer)
        {
            PrintCustomerInfo(customer);
            foreach(var invoice in invoices)
            {
                Console.WriteLine("------------------------------------------------------------------------------- ");
                Console.WriteLine("| #     |  Pavadinimas, Autorius, Žanras, Albumas, Milisekundės, Kaina, Kiekis | ");
                Console.WriteLine("------------------------------------------------------------------------------- ");
                foreach(var item in invoice.InvoiceItems)
                {
                    Console.WriteLine($"| {item.TrackId, 4}. | {item.Track.Name}, {item.Track.Composer}," +
                        $" {item.Track.Genre.Name}, {item.Track.Album.Title}, {item.Track.Milliseconds}, {item.Track.Price}, {item.Quantity} |  ");
                }
                Console.WriteLine("------------------------------------------------------------------------------- ");
                var totalWithoutTax = invoice.InvoiceItems.Sum(i => decimal.Multiply(i.Track.Price, i.Quantity));
                Console.WriteLine($"Visa suma be mokesčių: {totalWithoutTax}");
                Console.WriteLine($"Mokesčiai: {invoice.TotalPrice - totalWithoutTax}");
                Console.WriteLine($"Iš viso: {invoice.TotalPrice}");
            }
            Console.WriteLine("------------------------------------------------------------------------------- ");
        }

        public void PrintEmployeeSettingsMenu()
        {
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |   Pasirinkimas                     | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| 1.  |   Keisti klientų duomenis          |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2.  |   Pakeisti dainos statusą          |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3.  |   Statistika (Darbuotojams)        |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 4.  |   Grįžti atgal                     |  ");
            Console.WriteLine("--------------------------------------------------------------");
        }

        public void PrintChangeCustomerDataMenu()
        {
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |   Pasirinkimas                    | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| 1.  |   Gauti pirkėjų sąrašą            |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2.  |   Pašalinti pirkėją pagal ID      |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3.  |   Modifikuoti pirkėjo duomenis    |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 4.  |   Grįžti atgal                    |  ");
            Console.WriteLine("--------------------------------------------------------------");
        }

        public void PrintChangeSongStatusMenu()
        {
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| #   |   Pasirinkimas                    | ");
            Console.WriteLine("-------------------------------------------------------------- ");
            Console.WriteLine("| 1.  |   Gauti dainų sąrašą              |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 2.  |   Keisti dainos statusą           |  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("| 3.  |   Grįžti atgal                    |  ");
            Console.WriteLine("--------------------------------------------------------------");
        }

        public void PrintTracks(List<Track> tracks)
        {
            Console.WriteLine("------------------------------------------------------------------------------- ");
            Console.WriteLine("| #     |  Pavadinimas, Autorius, Milisekundės, Kaina, Statusas                | ");
            Console.WriteLine("------------------------------------------------------------------------------- ");
            foreach (var track in tracks)
                Console.WriteLine($"| {track.TrackId,4}. |  {track.Name}, {track.Composer}, {track.Milliseconds}," +
                    $"{track.Price}, {track.Status} |");
            Console.WriteLine("------------------------------------------------------------------------------- ");
        }
    }
}
