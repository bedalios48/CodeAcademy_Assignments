namespace NET_Mokymai
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            /*PARAŠYTI PROGRAMĄ KURIOJE SAUGOME :
            • MOKYKLOS PAVADINIMĄ
            • KURSO PAVADINIMĄ
            • STUDENTŲ SKAIČIŲ
            • ŠIANDIENOS DATĄ
            • VISUS KINTAMUOSIUS IŠVESTI Į EKRANĄ*/
            /*
            string mokykla = "Code Academy";
            string kursas = "CA .NET";
            int studentuSkaicius = 19;
            DateTime siandien = DateTime.Now;

            Console.WriteLine(mokykla);
            Console.WriteLine(kursas);
            Console.WriteLine(studentuSkaicius);
            Console.WriteLine(siandien.ToString("yyyy-MM-dd"));
            */
            /*PAPILDYTI PROGRAMĄ IR PRIDĖTI:
            • KURSO PRADŽIOS DATĄ
            • KURSO PABAIGOS DATĄ
            • Sužinoti skirtumą tarp pradžios ir dabartinės datos (dienomis)
            • VISUS KINTAMUOSIUS IŠVESTI Į EKRANĄ*/
            /*
            DateTime kursuPradzia = new DateTime(2022, 05, 30);
            DateTime kursuPabaiga = new DateTime(2023, 01, 01);
            var skirtumas = (kursuPabaiga - kursuPradzia).TotalDays;

            Console.WriteLine(kursuPradzia.ToString("yyyy-MM-dd"));
            Console.WriteLine(kursuPabaiga.ToString("yyyy-MM-dd"));
            Console.WriteLine(skirtumas);
            */
            /*Sukurkite tris kintamuosius. tekstinio, sveiko skaitmens ir loginio tipo.
            Parašykite programą kuri į konsolę visus aprašytus kintamuosius vienoje eilutėje atskiriant tarpu*/
            /*
            var tekstas = "Tekstas";
            var skaitmuo = 6;
            var taip = false;

            Console.WriteLine("{0} {1} {2}", tekstas, skaitmuo, taip);
            */
            /*PARAŠYTI PROGRAMĄ, KURIOJE VARTOTOJO PRAŠOMA ĮVESTI 2 SKAIČIUS.PROGRAMA TURI IŠVESTI
            • SKAIČIŲ SUMĄ
            • SKAIČIŲ SKIRTUMĄ
            • SANDAUGĄ
            • DALYBĄ*/

            /*
            Console.WriteLine("Iveskite 2 skaicius:");
            Console.Write("1:   ");
            var pirmas = Console.ReadLine();
            Console.Write("2:   ");
            var antras = Console.ReadLine();
            var pirmasDouble = double.Parse(pirmas);
            var antrasDouble = double.Parse(antras);
            Console.WriteLine("Skaiciu suma:    {0}", pirmasDouble + antrasDouble);
            Console.WriteLine("Skaiciu skirtumas:    {0}", pirmasDouble - antrasDouble);
            Console.WriteLine("Skaiciu sandauga:    {0}", pirmasDouble * antrasDouble);
            Console.WriteLine("Skaiciu dalyba:    {0}", pirmasDouble / antrasDouble);
            */

            /*PARAŠYTI PROGRAMĄ, 3 SKAIČIUS. PROGRAMA TURI IŠVESTI ŠIŲ SKAIČIŲ VIDURKĮ*/
            /*
            Console.WriteLine("Iveskite 3 skaicius:");
            Console.Write("1:   ");
            var pirmas = Console.ReadLine();
            Console.Write("2:   ");
            var antras = Console.ReadLine();
            Console.Write("3:   ");
            var trecias = Console.ReadLine();
            var pirmasDouble = double.Parse(pirmas);
            var antrasDouble = double.Parse(antras);
            var treciasDouble = double.Parse(trecias);
            Console.WriteLine("Skaiciu vidurkis:    `{0}", (pirmasDouble+antrasDouble+treciasDouble)/3);
            */
            /*
            sukurkite naują kintamajį long ir prskirkite didžiausią reikšmę.
            sukurkite naują kintamajį short ir prskirkite didžiausią reikšmę
            - padalinkite didesnį skaičių iš mažesnio
            - iš rezultato atimkite maksimalų long skaičių
            - ir pridėkite maksimalų int skaičių
            */
            var didziausiasLong = long.MaxValue;
            var didziausiasShort = short.MaxValue;
            var dalmuo = didziausiasLong / didziausiasShort;
            Console.WriteLine("Didziausio long ir short dalmuo: {0}", dalmuo);
            var skirtumas = dalmuo - didziausiasLong;
            Console.WriteLine("Toliau atimam didziausia long:   {0}", skirtumas);
            var didziausiasInt = int.MaxValue;
            var suma = skirtumas + didziausiasInt;
            Console.WriteLine("Tada pridedam didziausia int:    {0}", suma);
        }
    }
}