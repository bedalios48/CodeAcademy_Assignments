namespace GenealogyTree.Domain.Models
{
    public class ParentChild
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public virtual Person Parent { get; set; }
        public virtual Person Child { get; set; }
    }
}
