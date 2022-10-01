using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Models.Abstract
{
    public abstract class Logas
    {
        public IrasoId IrasoId { get; set; }
        public List<string> DiskuPozicijos { get; set; }
    }
}
