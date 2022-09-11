using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Interfaces
{
    public interface IKonsolesServisas
    {
        void AtspausdintiZaidima(ZaidimoBusena zaidimoBusena);
        int IvestiesPriemimas(ZaidimoBusena zaidimoBusena);
        void PranestiApieBlogaKonfiguracija();
        void AtspausdintiStatistikosMeniu();
        int PriimtiSkaiciu(int max);
        void AtspausdintiStatistika(List<ZaidimoStatistika> zaidimuStatistikos, int ataskaitosTipas);
        bool ArTestiZaidima();
    }
}