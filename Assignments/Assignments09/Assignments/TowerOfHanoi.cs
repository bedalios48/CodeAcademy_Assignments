using Assignments09.Domain.Interfaces;
using Assignments09.Domain.Models.Concrete;
using Assignments09.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace Assignments09.Assignments
{
    public class TowerOfHanoi
    {
        private List<ILoginimoServisas> _loginimoServisai;
        private IKonsolesServisas _konsole = new KonsolesServisas();
        private ILoguInformacijosServisas _loguInformacija = new LoguInformacijosServisas();
        
        public void ZaistiZaidima()
        {
            Inicializacija();
            var zaidimoBusena = new ZaidimoBusena();
            _konsole.AtspausdintiZaidima(zaidimoBusena);

            while (!zaidimoBusena.Iseiti)
            {
                var stulpelioNr = _konsole.IvestiesPriemimas(zaidimoBusena);
                if (zaidimoBusena.Iseiti)
                    continue;
                if(zaidimoBusena.Pagalba)
                {
                    var loginimoServisai = new List<ILoginimoServisas>
                    {
                        new CsvLoginimoServisas(),
                        new HtmlLoginimoServisas(),
                        new TxtLoginimoServisas()
                    };
                    zaidimoBusena.RekomenduojamasEjimas = _loguInformacija.GautiPagalba(zaidimoBusena, loginimoServisai);
                    _konsole.AtspausdintiZaidima(zaidimoBusena);
                    continue;
                }
                if(zaidimoBusena.Statistika)
                {
                    _konsole.AtspausdintiStatistikosMeniu();
                    var meniuPunktas = _konsole.PriimtiSkaiciu(2);
                    var loginimoServisai = new List<ILoginimoServisas>
                    {
                        new TxtLoginimoServisas(),
                        new HtmlLoginimoServisas(),
                        new CsvLoginimoServisas()
                    };
                    var zaidimuStatistikos = _loguInformacija.IstrauktiZaidimuStatistikas(loginimoServisai);
                    _konsole.AtspausdintiStatistika(zaidimuStatistikos, meniuPunktas);
                    if(_konsole.ArTestiZaidima())
                    {
                        zaidimoBusena.Statistika = false;
                        _konsole.AtspausdintiZaidima(zaidimoBusena);
                        continue;
                    }
                    Environment.Exit(0);
                }
                zaidimoBusena.StulpelioNr = stulpelioNr;
                Zaisti(zaidimoBusena);
                _konsole.AtspausdintiZaidima(zaidimoBusena);
                if(zaidimoBusena.PavykesEjimas)
                {
                    foreach (var loginimoServisas in _loginimoServisai)
                        loginimoServisas.IrasytiILoga(zaidimoBusena);
                }
            }
        }

        private void Inicializacija()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"configs\";
            var config = new ConfigurationBuilder()
               .SetBasePath(path)
               .AddXmlFile("game.config").Build();
            var section = config.GetSection("LoginimoNustatymai:LoginimoServisuIjungimas");
            var data = section.Get<LoginimoServisuIjungimas>();
            _loginimoServisai = new List<ILoginimoServisas>();
            if (data.Csv)
                _loginimoServisai.Add(new CsvLoginimoServisas());
            if (data.Html)
                _loginimoServisai.Add(new HtmlLoginimoServisas());
            if (data.Txt)
                _loginimoServisai.Add(new TxtLoginimoServisas());
            if (!(data.Csv || data.Html || data.Txt))
                _konsole.PranestiApieBlogaKonfiguracija();
        }

        private void Zaisti(ZaidimoBusena zaidimoBusena)
        {
            if (!zaidimoBusena.ArGeraIvestis)
                return;
            
            var stulpelis = zaidimoBusena.Stulpeliai[zaidimoBusena.StulpelioNr - 1];
            stulpelis.Diskai.Sort();

            if (zaidimoBusena.Paemimas)
            {
                if(!stulpelis.Diskai.Any())
                {
                    zaidimoBusena.ArGeraiPaimta = false;
                    zaidimoBusena.StulpelioNr = 0;
                    return;
                }
                zaidimoBusena.ArGeraiPaimta = true;
                zaidimoBusena.TurimoDiskoDydis = stulpelis.Diskai.First();
                stulpelis.Diskai.Remove(zaidimoBusena.TurimoDiskoDydis);
                zaidimoBusena.TxtLogas.DiskoDydis = zaidimoBusena.TurimoDiskoDydis;
                zaidimoBusena.TxtLogas.IsKurioStulpelioPaimta = zaidimoBusena.StulpelioNr;
                zaidimoBusena.Paemimas = false;
                return;
            }

            if (!(stulpelis.Diskai.Count == 0 || stulpelis.Diskai.Last() > zaidimoBusena.TurimoDiskoDydis))
            {
                zaidimoBusena.ArGeraiPadeta = false;
                zaidimoBusena.StulpelioNr = 0;
                return;
            }
            zaidimoBusena.ArGeraiPadeta = true;
            stulpelis.Diskai.Add(zaidimoBusena.TurimoDiskoDydis);
            zaidimoBusena.TurimoDiskoDydis = -1;
            zaidimoBusena.Paemimas = true;
            zaidimoBusena.TxtLogas.IKuriStulpeliPadeta = zaidimoBusena.StulpelioNr;
            zaidimoBusena.StulpelioNr = 0;
            zaidimoBusena.EjimoNr++;
        }
    }
}
