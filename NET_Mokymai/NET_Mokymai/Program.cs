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
            /*
            var didziausiasLong = long.MaxValue;
            var didziausiasShort = short.MaxValue;
            var dalmuo = didziausiasLong / didziausiasShort;
            Console.WriteLine("Didziausio long ir short dalmuo: {0}", dalmuo);
            var skirtumas = dalmuo - didziausiasLong;
            Console.WriteLine("Toliau atimam didziausia long:   {0}", skirtumas);
            var didziausiasInt = int.MaxValue;
            var suma = skirtumas + didziausiasInt;
            Console.WriteLine("Tada pridedam didziausia int:    {0}", suma);
            */

            /*Robertas Ūselis
            PARAŠYTI PROGRAMĄ KURI DIDELĮ 10 ŽENKLĮ SKAIČIŲ DOUBLE, KONVERTUOJA Į
            INT , SHORT , BYTE
            STEBĖTI REZULTATĄ.
            */

            /*
            var didelisDouble = (double)7891567258;
            Console.WriteLine("Skaicius double: {0}", didelisDouble);
            Console.WriteLine("Skaicius int:    {0}", (int)didelisDouble);
            Console.WriteLine("Skaicius short:  {0}", (short)didelisDouble);
            Console.WriteLine("Skaicius byte:   {0}", (byte)didelisDouble);
            */

            /*PARAŠYTI PROGRAMĄ KURI
            PRAŠO ĮVESTI RUTULIO DIAMETRĄ,
            O IŠVEDA PLOTĄ IR TŪRĮ*/

            /*
            Console.Write("Iveskite diametra:   ");
            var diametras = int.Parse(Console.ReadLine());
            Console.WriteLine("Pavirsiaus plotas:   {0}", 4*3.14*diametras*diametras);
            Console.WriteLine("Turis:   {0}", 4.0 / 3 * 3.14 * diametras * diametras * diametras);
            */

            /*PARAŠYTI PROGRAMĄ KURI PRAŠO ĮVESTI ATSTUMĄ (METRAIS) IR LAIKĄ (SEKUNDĖMIS),
            - IŠVEDA GREITĮ km/h.
            - IŠVEDA GREITĮ km/s.*/

            /*
            Console.Write("Iveskite atstuma:    ");
            var atstumas = double.Parse(Console.ReadLine());
            Console.Write("Iveskite laika:  ");
            var laikas = double.Parse(Console.ReadLine());
            var atstumasKm = atstumas / 1000;
            var laikasH = laikas / 60 / 60;
            Console.WriteLine("Greitis km/h:    {0}", atstumasKm/laikasH);
            Console.WriteLine("Greitis km/s:    {0}", atstumasKm / laikas);
            */

            /*Nuskaitykite iš klaviatūros 2 skaičius (x ir y).
            Išveskite į ekraną funkciją y+2y+x+1 ir apskaičiuokite šios funkcijos rezultatą.
            Išveskite į ekraną funkciją y²+x/2 apskaičiuokite šios funkcijos rezultatą.*/

            Console.Write("Iveskite x:  ");
            var x = double.Parse(Console.ReadLine());
            Console.Write("Iveskite y:  ");
            var y = double.Parse(Console.ReadLine());
            Console.WriteLine("Lygties \"y+2y+x+1\" rezultatas: {0}", y + 2*y + x + 1);
            Console.WriteLine("Lygties \"y^2 + x/2\" rezultatas: {0}", y *y+x/2);
        }
    }
}