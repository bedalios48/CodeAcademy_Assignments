using Assignments09.Domain.Models.Abstract;
using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Interfaces
{
    public interface ILoginimoServisas
    {
        void IrasytiILoga(ZaidimoBusena zaidimoStadija);
        List<Logas> NuskaitytiLoga();
    }
}
