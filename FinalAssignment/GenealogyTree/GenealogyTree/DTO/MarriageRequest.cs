namespace GenealogyTree.DTO
{
    public class MarriageRequest
    {
        public int PersonId { get; set; }
        public int SpouseId { get; set; }
        public string MarriageDate { get; set; }
        public bool AreDivorced { get; set; }
        public string DivorceDate { get; set; }
    }
}