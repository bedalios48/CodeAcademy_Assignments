namespace NET_Mokymai
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Ivedamos eilutes pradzios vertes
            var pirmosEilutesPradzia = "1eil.    ";
            var antrosEilutesPradzia = "2eil.    ";
            var treciosEilutesPradzia = "3eil.    ";
            var ketvirtosEilutesPradzia = "4eil.    ";
            var penktosEilutesPradzia = "5eil.    ";

            //Ivedamos bokstu konstrukcijos
            var tuscias = "     |     ";
            var vienas = "    #|#    ";
            var du = "   ##|##   ";
            var trys = "  ###|###  ";
            var keturi = " ####|#### ";
            var apacia = "\t----1stulp-------2stulp-------3stulp----";

            // Pirma uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, vienas, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, du, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, trys, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, tuscias, tuscias);
            Console.WriteLine(apacia);
            Console.WriteLine();

            //Antra uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, keturi, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, trys, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, du, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, vienas, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Trecia uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Ketvirta uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, keturi, keturi);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Penkta uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, tuscias, tuscias, vienas);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, trys, du);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Sesta uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, vienas, tuscias, vienas);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, trys, du);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Septinta uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, du, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, tuscias, du, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, tuscias, du, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, vienas, du, vienas);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, du, du);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Astunta uzduotis
            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, tuscias, tuscias, vienas);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, tuscias, tuscias, du);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, tuscias, tuscias, trys);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, tuscias, tuscias, keturi);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Devinta uzduotis
            vienas = vienas.Replace('#', '"');
            du = du.Replace('#', '"');
            trys = trys.Replace('#', '"');
            keturi = keturi.Replace('#', '"');

            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, tuscias, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, vienas, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, du, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, trys, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, tuscias, tuscias);
            Console.WriteLine(apacia);
            Console.WriteLine();

            // Desimta uzduotis
            Console.WriteLine("Iveskite pirmo stulpelio pirma eilute:");
            var ivestaVerte = Console.ReadLine();

            Console.WriteLine("{0} {1}  {2}  {3}", pirmosEilutesPradzia, ivestaVerte, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", antrosEilutesPradzia, vienas, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", treciosEilutesPradzia, du, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", ketvirtosEilutesPradzia, trys, tuscias, tuscias);
            Console.WriteLine("{0} {1}  {2}  {3}", penktosEilutesPradzia, keturi, tuscias, tuscias);
            Console.WriteLine(apacia);
            Console.WriteLine();
        }
    }
}