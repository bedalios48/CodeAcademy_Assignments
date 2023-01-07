using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<bool> TryLoginAsync(string userName, string password, out User user);
        Task<bool> ExistsAsync(string userName);
        Task<int> Register(User user);
    }
}