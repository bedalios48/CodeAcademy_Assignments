using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.Infrastructure.Data;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IPasswordService _password;
        public UserRepository(GenealogyTreeContext db, IPasswordService password) : base(db)
        {
            _password = password;
        }

        public async Task<User?> TryLoginAsync(string userName, string password)
        {
            var user = await GetAsync(u => u.UserName == userName);
            if(user is null)
                return null;

            if(_password.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return user;

            return null;
        }
    }
}
