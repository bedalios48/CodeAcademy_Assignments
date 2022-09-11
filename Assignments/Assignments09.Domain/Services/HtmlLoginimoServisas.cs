using Assignments09.Domain.Interfaces;
using Assignments09.Domain.Models.Abstract;
using Assignments09.Domain.Models.Concrete;
using System.Text;

namespace Assignments09.Domain.Services
{
    public class HtmlLoginimoServisas : ILoginimoServisas
    {
        private string _failoVieta = @"C:\Users\tamul\source\repos\NET_Mokymai\Assignments\Logs\TowerOfHanoi.html";
        public void IrasytiILoga(ZaidimoBusena zaidimoBusena)
        {
            var htmlLogas = new HtmlLogas
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

            if (!File.Exists(_failoVieta))
                SukurtiFailaSuAntraste();

            var failoLogai = NuskaitytiFailoEilutes();
            var logai = GautiInformacijaIsFailoLogo(failoLogai);
            var htmlLogai = SudetiInformacijaILogus(logai);
            htmlLogai.Add(htmlLogas);

            using StreamWriter sw = File.CreateText(_failoVieta);
            sw.AutoFlush = true;
            sw.WriteLine("<table border>");
            foreach (var logas in htmlLogai)
                sw.Write(GautiLogoTeksta(logas));
            sw.WriteLine("</table>");
        }

        public string GautiLogoTeksta(Logas logas)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<tr>");
            sb.Append("<th>");
            sb.Append(logas.IrasoId.Data);
            sb.AppendLine("</td>");
            sb.Append("<th>");
            sb.Append(logas.IrasoId.EjimoNr);
            sb.AppendLine("</td>");
            foreach (var diskas in logas.DiskuPozicijos)
            {
                sb.Append("<th>");
                sb.Append(diskas);
                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");

            return sb.ToString();
        }

        public List<Logas> NuskaitytiLoga()
        {
            var failoLogai = NuskaitytiFailoEilutes();
            PasalintiAntraste(failoLogai);
            var logai = GautiInformacijaIsFailoLogo(failoLogai);
            return SudetiInformacijaILogus(logai);
        }

        private List<string> NuskaitytiFailoEilutes()
        {
            if (!File.Exists(_failoVieta))
                return new List<string>();
            return File.ReadAllLines(_failoVieta).ToList();
        }

        private void PasalintiAntraste(List<string> failoLogai)
        {
            failoLogai.Remove("<th>ŽAIDIMO PRADŽIOS DATA</td>");
            failoLogai.Remove("<th>ĖJIMO NR</td>");
            failoLogai.Remove("<th>DISKO 1 VIETA</td>");
            failoLogai.Remove("<th>DISKO 2 VIETA</td>");
            failoLogai.Remove("<th>DISKO 3 VIETA</td>");
            failoLogai.Remove("<th>DISKO 4 VIETA</td>");
        }

        private List<string> GautiInformacijaIsFailoLogo(List<string> failoLogai)
        {
            failoLogai.Remove("<table border>");
            failoLogai.Remove("</table>");

            var logai = new List<string>();
            foreach (var logas in failoLogai)
            {
                if (!logas.Contains("tr"))
                    logai.Add(logas.Replace("<th>", "").Replace("</td>", ""));
            }
            return logai;
        }

        private List<Logas> SudetiInformacijaILogus(List<string> logai)
        {
            var htmlLogai = new List<Logas>();
            for (int i = 0; i < logai.Count; i += 6)
            {
                htmlLogai.Add(new HtmlLogas
                {
                    IrasoId = new IrasoId
                    {
                        Data = logai[i],
                        EjimoNr = logai[i + 1]
                    },
                    DiskuPozicijos = new List<string>
                        {
                            logai[i + 2],
                            logai[i + 3],
                            logai[i + 4],
                            logai[i + 5]
                        }
                });
            }

            return htmlLogai;
        }

        private void SukurtiFailaSuAntraste()
        {
            using StreamWriter sw = File.AppendText(_failoVieta);
            sw.AutoFlush = true;
            sw.WriteLine("<table border>");
            sw.WriteLine("<tr>");
            sw.WriteLine("<th>ŽAIDIMO PRADŽIOS DATA</td>");
            sw.WriteLine("<th>ĖJIMO NR</td>");
            sw.WriteLine("<th>DISKO 1 VIETA</td>");
            sw.WriteLine("<th>DISKO 2 VIETA</td>");
            sw.WriteLine("<th>DISKO 3 VIETA</td>");
            sw.WriteLine("<th>DISKO 4 VIETA</td>");
            sw.WriteLine("</tr>");
            sw.WriteLine("</table>");
        }
    }
}
