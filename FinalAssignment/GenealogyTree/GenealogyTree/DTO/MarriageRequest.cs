namespace GenealogyTree.DTO
{
    public class MarriageRequest
    {
        public int PersonId { get; set; }
        public int SpouseId { get; set; }
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public string MarriageDate { get; set; }
        /// <summary>
        /// true or false
        /// </summary>
        public bool AreDivorced { get; set; }
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public string DivorceDate { get; set; }
    }
}