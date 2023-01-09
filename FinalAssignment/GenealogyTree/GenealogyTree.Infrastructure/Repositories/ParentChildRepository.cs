using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.Infrastructure.Data;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class ParentChildRepository : Repository<ParentChild>, IParentChildRepository
    {
        public ParentChildRepository(GenealogyTreeContext db) : base(db)
        {
        }
    }
}
