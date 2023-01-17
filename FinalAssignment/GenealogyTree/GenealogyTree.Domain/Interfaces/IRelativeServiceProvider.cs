using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Interfaces
{
    public interface IRelativeServiceProvider
    {
        Task<IEnumerable<Relative>> GetChildrenAsync(int parentId, int generation);
        Task<IEnumerable<Relative>> GetParentsAsync(int childId, int generation);
        Task<IEnumerable<Relative>> GetSiblingsAsync(int personId);
        Task<IEnumerable<Relative>> GetSpousesAsync(int personId);
    }
}