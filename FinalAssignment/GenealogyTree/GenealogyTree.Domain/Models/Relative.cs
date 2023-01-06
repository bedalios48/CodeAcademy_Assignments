namespace GenealogyTree.Domain.Models
{
    public class Relative
    {
        public Relative(Person person, string relation)
        {
            Person = person;
            Relation = relation;
        }
        public Person Person { get; set; }
        public MainRelative MainRelative { get; set; }
        public string Relation { get; set; }
    }
}
