using Assignments07;
using Assignments07.Assignments;

namespace Assignments07.Tests
{
    [TestClass]
    public class SuperSkaiciuotuvoUzduotisTests
    {
        [TestMethod()]
        public void SuperSkaiciuotuvasTest1()
        {
            var fake_moves = new string[] { "1", "1", "15", "45", "2", "2", "10", "3" };
            var expected = 50d;
            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var actual = SuperSkaiciuotuvoUzduotis.Rezultatas();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SuperSkaiciuotuvasTest2()
        {
            var fake_moves = new string[] { "1", "1", "15", "45", "3" };
            var expected = 60d;
            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var actual = SuperSkaiciuotuvoUzduotis.Rezultatas();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SuperSkaiciuotuvasTest3()
        {
            var fake_moves = new string[] { "1", "1", "15", "45", "2", "2", "10", "1", "3", "2", "3", "3" };
            var expected = 6d;

            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var actual = SuperSkaiciuotuvoUzduotis.Rezultatas();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GautiTinkamusMeniuPunktus_GrazinaPunktuSarasa1()
        {
            var fake_moves = new string[] { "1"};
            var expected = new List<int> { 1, 2, 3, 4, 5, 6 };
            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var result = SuperSkaiciuotuvoUzduotis.GautiTinkamusMeniuPunktus();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GautiTinkamusMeniuPunktus_GrazinaPunktuSarasa2()
        {
            var fake_moves = new string[] { "1", "1", "1" };
            var expected = new List<int> { 1, 2, 3 };
            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var result = SuperSkaiciuotuvoUzduotis.GautiTinkamusMeniuPunktus();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PaprasytiSkaiciaus_GrazinaIvestiesTeksta1()
        {
            var fake_moves = new string[] { "1", "1", "1" };
            var expected = "Iveskite antra skaiciu:";
            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var result = SuperSkaiciuotuvoUzduotis.PaprasytiSkaiciaus();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PaprasytiSkaiciaus_GrazinaIvestiesTeksta2()
        {
            var fake_moves = new string[] { "1" };
            var expected = "1. Sudetis" + Environment.NewLine +
                "2. Atimtis" + Environment.NewLine +
                "3. Daugyba" + Environment.NewLine +
                "4. Dalyba" + Environment.NewLine +
                "5. Saknies traukimas" + Environment.NewLine +
                "6. Kelimas laipsniu";
            SuperSkaiciuotuvoUzduotis.Reset();
            foreach (var move in fake_moves)
            {
                SuperSkaiciuotuvoUzduotis.SuperSkaiciuotuvas(move);
            }
            var result = SuperSkaiciuotuvoUzduotis.PaprasytiSkaiciaus();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Sudetis_GrazinaSudeti()
        {
            var fake1 = 8;
            var fake2 = 9.9;
            var expected = 17.9;
            var result = SuperSkaiciuotuvoUzduotis.Sudetis(fake1, fake2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Atimtis_GrazinaAtimti()
        {
            var fake1 = 8;
            var fake2 = 9.9;
            var expected = 8 - 9.9;
            var result = SuperSkaiciuotuvoUzduotis.Atimtis(fake1, fake2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Daugyba_GrazinaSandauga()
        {
            var fake1 = 8;
            var fake2 = 9.9;
            var expected = 79.2;
            var result = SuperSkaiciuotuvoUzduotis.Daugyba(fake1, fake2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Dalyba_GrazinaDalmeni()
        {
            var fake1 = 8;
            var fake2 = 9.9;
            var expected = 8 / 9.9;
            var result = SuperSkaiciuotuvoUzduotis.Dalyba(fake1, fake2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SakniesTraukimas_GrazinaSakni()
        {
            var fake1 = 8;
            var fake2 = 3;
            var expected = 2;
            var result = SuperSkaiciuotuvoUzduotis.SakniesTraukimas(fake1, fake2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LaipsnioKelimas_GrazinaLaipsniuPakeltaSkaiciu()
        {
            var fake1 = 8;
            var fake2 = 0;
            var expected = 1;
            var result = SuperSkaiciuotuvoUzduotis.LaipsnioKelimas(fake1, fake2);

            Assert.AreEqual(expected, result);
        }
    }
}