using GenealogyTree.Domain.Interfaces.IRepositories;

namespace GenealogyTree.Domain.Models
{
    public class Marriage : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int SpouseId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? MarriedDate { get; set; }
        public DateTime? DivorcedDate { get; set; }
        public bool AreDivorced { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person SpousePerson { get; set; }
    }
}
