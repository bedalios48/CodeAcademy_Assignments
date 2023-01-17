using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Infrastructure.Data;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GenealogyTreeContext _db;

        public UnitOfWork(GenealogyTreeContext db,
            IParentChildRepository parentChild,
            IPersonRepository person,
            IUserRepository user,
            IMarriageRepository spouse)
        {
            _db = db;
            ParentChild = parentChild;
            Person = person;
            User = user;
            Marriage = spouse;
        }

        public IParentChildRepository ParentChild { get; private set; }

        public IPersonRepository Person { get; private set; }

        public IUserRepository User { get; private set; }

        public IMarriageRepository Marriage { get; private set; }
    }
}
