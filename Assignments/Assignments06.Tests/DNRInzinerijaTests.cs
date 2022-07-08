using Assignments06.Assignments;

namespace Assignments06.Tests
{
    [TestClass]
    public class DNRInzinerijaTests
    {
        [TestMethod]
        [DataRow("AAA", true)]
        [DataRow("AAAA", false)]
        public void SegmentoIlgisValiduojamas(string fake, bool expected)
        {
            var result = DNRInzinerija.ArValidusSegmentas(fake);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("AAA", true)]
        [DataRow("BBB", false)]
        public void SegmentoRaidesValiduojamos(string fake, bool expected)
        {
            var result = DNRInzinerija.ArValidusSegmentas(fake);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GCTPakeistaIAGG()
        {
            var fake = "GCT-CCC";
            var expected = "AGG-CCC";
            var result = DNRInzinerija.GCTiAGG(fake);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("CAT-GGG", true)]
        [DataRow("CCC-GGG", false)]
        public void PatikrinaArYraCAT(string fake, bool expected)
        {
            var result = DNRInzinerija.ArTeksteYraCAT(fake);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ArVeikiaGrandinesNormalizavimas()
        {
            var fake = "   aaa-   ccc-t  at";
            var expected = "AAA-CCC-TAT";
            var dnrInzinerija = new DNRInzinerija();
            var result = dnrInzinerija.AtliktiGrandinesNormalizavima(fake);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("CAT-GGG", true)]
        [DataRow("CCC-LLL", false)]
        public void ArVeikiaGrandinesValidacija(string fake, bool expected)
        {
            var result = DNRInzinerija.ArValidiGrandine(fake);
            Assert.AreEqual(expected, result);
        }
    }
}