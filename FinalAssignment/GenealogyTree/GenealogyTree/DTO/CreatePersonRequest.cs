using GenealogyTree.Domain.Enums;

namespace GenealogyTree.DTO
{
    public class CreatePersonRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// Male, Female, Other
        /// </summary>
        public string Sex { get; set; }
    }
}