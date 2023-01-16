using GenealogyTree.Domain.Models;

namespace GenealogyTree.Domain.Interfaces
{
    public interface IRelativeService
    {
        Task<MainRelative> GetMainRelativeAsync(int personId);
    }
}
