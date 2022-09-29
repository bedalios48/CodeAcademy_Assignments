using Assignments09_2.Domain.Models;

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
    }
}