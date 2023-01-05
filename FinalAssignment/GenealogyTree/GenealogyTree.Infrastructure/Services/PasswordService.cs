using GenealogyTree.Domain.Interfaces;

namespace GenealogyTree.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            passwordHash = new byte[32];
            passwordSalt = new byte[33];
        }
    }
}
