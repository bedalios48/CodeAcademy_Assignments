using Assignments09.Domain.Interfaces;
using Assignments09.Domain.Models.Abstract;
using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Services
{
    public class LoguInformacijosServisas : ILoguInformacijosServisas
    {
        public int[] GautiPagalba(ZaidimoBusena zaidimoBusena, List<ILoginimoServisas> loginimoServisai)
        {
            var zaidimuStatistikos = IstrauktiZaidimuStatistikas(loginimoServisai);
            var rekomenduojamasEjimas
                = new int[] { 0, 0 };
            var dabartinePozicija = zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(1)).First().StulpelioNr.ToString() +
                   zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(2)).First().StulpelioNr.ToString() +
                   zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(3)).First().StulpelioNr.ToString() +
                   zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(4)).First().StulpelioNr.ToString();

            var rekomenduojamaPozicija = GautiRekomenduojamaPozicija(zaidimuStatistikos, zaidimoBusena, dabartinePozicija);
            if (rekomenduojamaPozicija.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (rekomenduojamaPozicija[i] != dabartinePozicija[i].ToString())
                    {
                        rekomenduojamasEjimas[0] = int.Parse(dabartinePozicija[i].ToString());
                        rekomenduojamasEjimas[1] = int.Parse(rekomenduojamaPozicija[i]);
                    }
                }
            }

            return rekomenduojamasEjimas;
        }

        private List<Logas> IstrauktiVisusLogus(List<ILoginimoServisas> loginimoServisai)
        {
            var visiLogai = new List<Logas>();
            foreach (var loginimoServisas in loginimoServisai)
            {
                var logai = loginimoServisas.NuskaitytiLoga();
                foreach (var logas in logai)
                {
                    if (!visiLogai.Any(l => l.IrasoId.EjimoNr == logas.IrasoId.EjimoNr &&
                    l.IrasoId.Data == logas.IrasoId.Data))
                        visiLogai.Add(logas);
                }
            }

            return visiLogai;
        }

        private List<List<Logas>> SugrupuotiVisusZaidimus(List<Logas> visiLogai)
        {
            var visiZaidimai = new List<List<Logas>>();
            var index = 0;
            foreach (var logas in visiLogai.ToList())
            {
                if (visiZaidimai.Count < index + 1)
                    visiZaidimai.Add(new List<Logas>());
                visiZaidimai[index].Add(logas);
                visiLogai.Remove(logas);
                if (visiLogai.FirstOrDefault() != null && visiLogai.First().IrasoId.EjimoNr == "1")
                    index++;
            }

            return visiZaidimai;
        }

        private List<ZaidimoStatistika> GautiZaidimuStatistikas(List<List<Logas>> visiZaidimai)
        {
            var zaidimuStatistikos = new List<ZaidimoStatistika>();
            foreach (var zaidimas in visiZaidimai)
            {
                var ejimai = new List<Logas>();
                var ejimoNr = 1;
                foreach (var ejimas in zaidimas)
                {
                    ejimai.Add(ejimas);
                    var padetys = string.Join("", ejimas.DiskuPozicijos);
                    if (padetys == "2222" || padetys == "3333")
                    {
                        zaidimuStatistikos.Add(new ZaidimoStatistika
                        {
                            Laimetas = true,
                            Ejimai = ejimai
                        });
                        break;
                    }
                    if (ejimoNr == zaidimas.Count)
                    {
                        zaidimuStatistikos.Add(new ZaidimoStatistika
                        {
                            Laimetas = false,
                            Ejimai = ejimai
                        });
                    }
                    ejimoNr++;
                }
            }
            return zaidimuStatistikos;
        }

        private List<string> GautiRekomenduojamaPozicija(List<ZaidimoStatistika> zaidimuStatistikos, ZaidimoBusena zaidimoBusena,
            string dabartinePozicija)
        {
            var trumpiausiasEjimas = int.MaxValue;
            var rekomenduojamaPozicija = new List<string>();

            foreach (var zaidimoStatistika in zaidimuStatistikos)
            {
                if (!zaidimoStatistika.Laimetas)
                    continue;

                var ejimai = zaidimoStatistika.Ejimai;
                foreach (var ejimas in ejimai)
                {
                    var ejimoBusena = string.Join("", ejimas.DiskuPozicijos);
                    if (ejimoBusena == dabartinePozicija)
                    {
                        if (ejimai.Count < trumpiausiasEjimas)
                        {
                            trumpiausiasEjimas = ejimai.Count;
                            var kitasEjimas = ejimai[ejimai.IndexOf(ejimas) + 1];
                            rekomenduojamaPozicija = kitasEjimas.DiskuPozicijos;
                        }
                    }
                }
            }

            return rekomenduojamaPozicija;
        }

        public List<ZaidimoStatistika> IstrauktiZaidimuStatistikas(List<ILoginimoServisas> loginimoServisai)
        {
            var visiZaidimai = SugrupuotiVisusZaidimus(IstrauktiVisusLogus(loginimoServisai));
            return GautiZaidimuStatistikas(visiZaidimai);
        }
    }
}
