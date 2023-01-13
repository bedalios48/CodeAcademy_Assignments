using GenealogyTree.Domain.Interfaces.IRepositories;

namespace GenealogyTree.Domain.Models
{
    public class ParentChild : IEntity
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public int CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual Person Parent { get; set; }
        public virtual Person Child { get; set; }
    }
}
