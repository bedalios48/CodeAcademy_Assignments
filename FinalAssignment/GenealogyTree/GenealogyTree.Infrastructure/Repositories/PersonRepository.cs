using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.Infrastructure.Data;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(GenealogyTreeContext db) : base(db)
        {
        }
    }
}
