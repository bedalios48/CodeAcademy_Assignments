using GenealogyTree.Domain.Interfaces.IRepositories;

namespace GenealogyTree.Domain.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual Person Person { get; set; }
        public virtual List<Person> CreatedPeople { get; set; }
        public virtual List<ParentChild> CreatedRelations { get; set; }
    }
}