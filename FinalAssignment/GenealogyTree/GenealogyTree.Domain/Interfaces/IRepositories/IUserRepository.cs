using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> TryLoginAsync(string userName, string password);
    }
}