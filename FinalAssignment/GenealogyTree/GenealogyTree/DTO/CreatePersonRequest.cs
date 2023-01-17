namespace GenealogyTree.DTO
{
    public class CreatePersonRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
    }
}