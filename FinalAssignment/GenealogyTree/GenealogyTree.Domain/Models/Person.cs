namespace GenealogyTree.Domain.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual List<ParentChild> Children { get; set; }
    }
}
