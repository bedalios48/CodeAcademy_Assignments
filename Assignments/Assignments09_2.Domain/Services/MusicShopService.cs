using Assignments09_2.Domain.Interfaces;

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
                    _console.PrintCustomers(_repository.GetCustomers());
            }
        }
    }
}
