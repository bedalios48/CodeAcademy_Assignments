using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> ExistsAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<int> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryLoginAsync(string userName, string password, out User user)
        {
            throw new NotImplementedException();
        }
    }
}
