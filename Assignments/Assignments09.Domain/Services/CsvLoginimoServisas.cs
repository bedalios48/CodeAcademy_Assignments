using Assignments09.Domain.Interfaces;
using Assignments09.Domain.Models.Abstract;
using Assignments09.Domain.Models.Concrete;
using System.Text;

namespace Assignments09.Domain.Services
{
    public class CsvLoginimoServisas : ILoginimoServisas
    {
        private string _failoVieta = @"C:\Users\tamul\source\repos\NET_Mokymai\Assignments\Logs\TowerOfHanoi.csv";
        public void IrasytiILoga(ZaidimoBusena zaidimoBusena)
        {
            var csvLogas = new CsvLogas
            {
                IrasoId = new IrasoId
                {
                    Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                    EjimoNr = zaidimoBusena.EjimoNr.ToString()
                },
                DiskuPozicijos = new List<string>
                {
                    zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(1)).First().StulpelioNr.ToString(),
                    zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(2)).First().StulpelioNr.ToString(),
                    zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(3)).First().StulpelioNr.ToString(),
                    zaidimoBusena.Stulpeliai.Where(s => s.Diskai.Contains(4)).First().StulpelioNr.ToString()
                }
            };

            using StreamWriter sw = File.AppendText(_failoVieta);
            sw.Write(GautiLogoTeksta(csvLogas));
        }

        public string GautiLogoTeksta(Logas logas)
        {
            var sb = new StringBuilder();
            sb.Append(logas.IrasoId.Data + ',');
            sb.Append(logas.IrasoId.EjimoNr + ',');
            sb.Append(logas.DiskuPozicijos[0] + ',');
            sb.Append(logas.DiskuPozicijos[1] + ',');
            sb.Append(logas.DiskuPozicijos[2] + ',');
            sb.AppendLine(logas.DiskuPozicijos[3]);
            return sb.ToString();
        }

        public List<Logas> NuskaitytiLoga()
        {
            List<Logas> csvLogai = new List<Logas>();
            if (!File.Exists(_failoVieta))
                return csvLogai;
            var logai = File.ReadAllLines(_failoVieta);
            foreach(var logas in logai)
            {
                var irasai = logas.Split(',');
                csvLogai.Add(new CsvLogas
                {
                    IrasoId = new IrasoId
                    {
                        Data = irasai[0],
                        EjimoNr = irasai[1]
                    },
                    DiskuPozicijos = new List<string>
                    {
                        irasai[2],
                        irasai[3],
                        irasai[4],
                        irasai[5]
                    }
                });
            }

            return csvLogai;
        }
    }
}
