using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Interfaces
{
    public interface ILoguInformacijosServisas
    {
        int[] GautiPagalba(ZaidimoBusena zaidimoBusena, List<ILoginimoServisas> loginimoServisai);
        List<ZaidimoStatistika> IstrauktiZaidimuStatistikas(List<ILoginimoServisas> loginimoServisai);
    }
}