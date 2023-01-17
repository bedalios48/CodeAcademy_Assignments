namespace GenealogyTree.Domain.Interfaces.IRepositories
{
    public interface IUnitOfWork
    {
        IParentChildRepository ParentChild { get; }
        IPersonRepository Person { get; }
        IUserRepository User { get; }
        IMarriageRepository Marriage { get; }
    }
}
