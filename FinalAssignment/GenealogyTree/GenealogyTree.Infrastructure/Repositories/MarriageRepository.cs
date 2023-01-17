using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Models;
using GenealogyTree.Infrastructure.Data;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class MarriageRepository : Repository<Marriage>, IMarriageRepository
    {
        public MarriageRepository(GenealogyTreeContext db) : base(db)
        {
        }
    }
}
