namespace GenealogyTree.Domain.Models
{
    public class MainRelative
    {
        public MainRelative(Person person)
        {
            Person = person;
        }
        public Person Person { get; set; }
        public List<Relative> Relatives { get; set; } = new List<Relative>();
    }
}
