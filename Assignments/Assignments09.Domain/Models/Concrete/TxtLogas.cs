using Assignments09.Domain.Models.Abstract;

namespace Assignments09.Domain.Models.Concrete
{
    public class TxtLogas : Logas
    {
        public string Data { get; set; }
        public int DiskoDydis { get; set; }
        public int IsKurioStulpelioPaimta { get; set; }
        public int IKuriStulpeliPadeta { get; set; }
    }
}
