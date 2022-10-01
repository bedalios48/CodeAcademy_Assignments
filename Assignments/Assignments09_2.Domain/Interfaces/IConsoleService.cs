using Assignments09_2.Domain.Models;
using Assignments09_2.Domain.Models.Database;

namespace Assignments09_2.Domain.Interfaces
{
    public interface IConsoleService
    {
        void PrintMainMenu();
        int GetNumber();
        void PrintCustomers(List<Customer> customers);
        void PrintString(string value);
        string GetValue();
        void ClearConsole();
        void PrintPurchaseMenu();
        void PrintTrackMenu(List<Track> tracks);
        void PrintDataOptions();
        string GetInput(string[] allowedValues);
        void PrintOrderOptions();
        void PrintSearchOptions();
        void PrintAddToBasketOptions();
        void PrintInvoiceInfo(Session session);
    }
}