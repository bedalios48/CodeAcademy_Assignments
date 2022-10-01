using Assignments09.Domain.Models.Abstract;

namespace Assignments09.Domain.Models.Concrete
{
    public class ZaidimoStatistika
    {
        public bool Laimetas { get; set; }
        public List<Logas> Ejimai { get; set; }
        public int EjimuSkaicius { get => Ejimai.Count; }
        public string ZaidimoData { get => Ejimai.Count > 0 ? Ejimai.First().IrasoId.Data : ""; }
    }
}
