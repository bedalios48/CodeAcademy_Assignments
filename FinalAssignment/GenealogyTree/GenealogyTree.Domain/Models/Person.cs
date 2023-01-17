using GenealogyTree.Domain.Enums;
using GenealogyTree.Domain.Interfaces.IRepositories;

namespace GenealogyTree.Domain.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BirthPlace { get; set; }
        public ESex Sex { get; set; } = ESex.Other;
        public int? UserId { get; set; }
        public int CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? User { get; set; }
        public virtual List<ParentChild> Children { get; set; } = new List<ParentChild>();
        public virtual List<ParentChild> Parents { get; set; } = new List<ParentChild>();
        public virtual List<Marriage> Spouses { get; set; } = new List<Marriage>();
        public virtual List<Marriage> Marriages { get; set; } = new List<Marriage>();
    }
}
