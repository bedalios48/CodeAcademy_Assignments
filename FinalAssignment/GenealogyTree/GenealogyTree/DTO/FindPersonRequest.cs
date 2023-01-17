namespace GenealogyTree.DTO
{
    public class FindPersonRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public string? DateOfBirth { get; set; }
        public string? BirthPlace { get; set; }
    }
}