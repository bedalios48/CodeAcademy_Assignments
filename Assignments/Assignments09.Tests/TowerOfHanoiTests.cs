using Assignments09.Domain.Models.Concrete;
using Assignments09.Domain.Services;

namespace Assignments09.Tests
{
    [TestClass]
    public class TowerOfHanoiTests
    {
        [TestMethod]
        public void CsvLoginimoServisas_GautiLogoTeksta_GrazinaLogoTeksta()
        {
            var csv = new CsvLoginimoServisas();
            var logas = new CsvLogas
            {
                IrasoId = new IrasoId
                {
                    Data = "test1",
                    EjimoNr = "test2"
                },
                DiskuPozicijos = new List<string>
                {
                    "1", "2", "3", "4"
                }
            };
            var expected = "test1,test2,1,2,3,4\r\n";
            var result = csv.GautiLogoTeksta(logas);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void HtmlLoginimoServisas_GautiLogoTeksta_GrazinaLogoTeksta()
        {
            var html = new HtmlLoginimoServisas();
            var logas = new HtmlLogas
            {
                IrasoId = new IrasoId
                {
                    Data = "test1",
                    EjimoNr = "test2"
                },
                DiskuPozicijos = new List<string>
                {
                    "1", "2", "3", "4"
                }
            };

            var expected = "<tr>\r\n<th>test1</td>\r\n<th>test2</td>\r\n<th>1</td>\r\n<th>2</td>\r\n<th>3</td>\r\n<th>4</td>\r\n</tr>\r\n";
            var result = html.GautiLogoTeksta(logas);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TxtLoginimoServisas_GautiLogoTeksta_GrazinaLogoTeksta()
        {
            var txt = new TxtLoginimoServisas();
            
            var busena = new ZaidimoBusena
            {
                TxtLogas = new TxtLogas
                {
                    Data = "test1",
                    DiskoDydis = 1,
                    IsKurioStulpelioPaimta = 1,
                    IKuriStulpeliPadeta = 1
                },
                EjimoNr = 1
            };

            var expected = "Žaidime, kuris pradėtas test1, " +
                "ėjimu nr 1 1 dalies diskas buvo paimtas iš " +
                "pirmo stulpelio ir padėtas į pirmą";
            var result = txt.GautiLogoTeksta(busena);
            Assert.AreEqual(expected, result);
        }
    }
}