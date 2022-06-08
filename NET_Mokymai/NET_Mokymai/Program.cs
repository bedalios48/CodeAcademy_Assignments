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

            string mokykla = "Code Academy";
            string kursas = "CA .NET";
            int studentuSkaicius = 19;
            DateTime siandien = DateTime.Now;

            Console.WriteLine(mokykla);
            Console.WriteLine(kursas);
            Console.WriteLine(studentuSkaicius);
            Console.WriteLine(siandien.ToString("yyyy-MM-dd"));

            /*PAPILDYTI PROGRAMĄ IR PRIDĖTI:
            • KURSO PRADŽIOS DATĄ
            • KURSO PABAIGOS DATĄ
            • Sužinoti skirtumą tarp pradžios ir dabartinės datos (dienomis)
            • VISUS KINTAMUOSIUS IŠVESTI Į EKRANĄ*/

            DateTime kursuPradzia = new DateTime(2022, 05, 30);
            DateTime kursuPabaiga = new DateTime(2023, 01, 01);
            var skirtumas = (kursuPabaiga - kursuPradzia).TotalDays;

            Console.WriteLine(kursuPradzia.ToString("yyyy-MM-dd"));
            Console.WriteLine(kursuPabaiga.ToString("yyyy-MM-dd"));
            Console.WriteLine(skirtumas);

            /*Sukurkite tris kintamuosius. tekstinio, sveiko skaitmens ir loginio tipo.
            Parašykite programą kuri į konsolę visus aprašytus kintamuosius vienoje eilutėje atskiriant tarpu*/

            var tekstas = "Tekstas";
            var skaitmuo = 6;
            var taip = false;

            Console.WriteLine("{0} {1} {2}", tekstas, skaitmuo, taip);
        }
    }
}