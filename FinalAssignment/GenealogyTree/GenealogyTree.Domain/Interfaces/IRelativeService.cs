using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Interfaces
{
    public interface IRelativeService
    {
        Task<MainRelative> GetMainRelative(int personId);
    }
}
