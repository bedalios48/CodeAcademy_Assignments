using Assignments09.Domain.Enums;
using Assignments09.Domain.Interfaces;
using Assignments09.Domain.Models.Abstract;
using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Services
{
    public class TxtLoginimoServisas : ILoginimoServisas
    {
        private string _failoVieta = @"C:\Users\tamul\source\repos\NET_Mokymai\Assignments\Logs\TowerOfHanoi.txt";
        public void IrasytiILoga(ZaidimoBusena zaidimoBusena)
        {
            using StreamWriter sw = File.AppendText(_failoVieta);
            zaidimoBusena.TxtLogas.Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            sw.WriteLine(GautiLogoTeksta(zaidimoBusena));
        }

        public string GautiLogoTeksta(ZaidimoBusena zaidimoBusena)
        {
            var isKurPaimta = (StulpelioNrKilmininkas)zaidimoBusena.TxtLogas.IsKurioStulpelioPaimta;
            var iKurPadeta = (StulpelioNrGalininkas)zaidimoBusena.TxtLogas.IKuriStulpeliPadeta;
            var daliesArDaliu = zaidimoBusena.TxtLogas.DiskoDydis == 1 ? "dalies" : "dalių";
            return $"Žaidime, kuris pradėtas {zaidimoBusena.TxtLogas.Data}, " +
                $"ėjimu nr {zaidimoBusena.EjimoNr} {zaidimoBusena.TxtLogas.DiskoDydis} {daliesArDaliu} diskas buvo paimtas iš " +
                $"{isKurPaimta} stulpelio ir padėtas į {iKurPadeta}";
        }

        public List<Logas> NuskaitytiLoga()
        {
            var txtLogai = new List<Logas>();
            if (!File.Exists(_failoVieta))
                return txtLogai;

            var failoLogai = File.ReadAllLines(_failoVieta).ToList();

            var diskuPozicijos = new List<string> { "1", "1", "1", "1" };
            foreach(var logas in failoLogai)
            {
                var logoTekstas = logas.Replace("Žaidime, kuris pradėtas", "")
                    .Replace("ėjimu nr", "")
                    .Replace("dalių diskas buvo paimtas iš ", "")
                    .Replace("dalies diskas buvo paimtas iš ", "")
                    .Replace("stulpelio ir padėtas į ", "")
                    .Replace(",", "");
                var logoElementai = logoTekstas.Split(" ").ToList();
                foreach(var elementas in logoElementai.ToList())
                    if(elementas == "")
                        logoElementai.Remove(elementas);
                if (logoElementai.Count != 6)
                    continue;

                if (int.Parse(logoElementai[2]) == 1)
                    diskuPozicijos = new List<string> { "1", "1", "1", "1" };

                var diskoNr = int.Parse(logoElementai[3]);
                var naujaVieta = (int)Enum.Parse(typeof(StulpelioNrGalininkas), logoElementai[5], true);
                diskuPozicijos[diskoNr - 1] = naujaVieta.ToString();
                txtLogai.Add(new TxtLogas
                {
                    IrasoId = new IrasoId
                    {
                        Data = logoElementai[0] + " " + logoElementai[1],
                        EjimoNr = logoElementai[2]
                    },
                    DiskuPozicijos = diskuPozicijos.ToList()
                });
            }

            return txtLogai;
        }
    }
}
