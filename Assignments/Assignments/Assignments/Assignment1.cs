namespace Assignments06.Assignments
{
    internal class Assignment1
    {
        public void Method()
        {
            /*
             Paprašykite naudotojo įvesti 1 skaičių - temperatūrą pagal Celsijų. 
               - Paskaičiuokite ir išveskite į ekraną temperatūrą pagal farenheitą.
               - Paskaičiuokite ir išveskite į ekraną temperatūrą pagal kelviną.
               - Gautą temperatūrą pagal farenheitą perskaičiuokite į Celsijų ir patikrinkite ar sutampa su įvestu skaičių (išveskite true/false)
               - Gautą temperatūrą pagal kelviną perskaičiuokite į celsijų ir patikrinkite ar sutampa su įvestu skaičiu (išveskite true/false)
               - Paskaičiuotą temperatūrą pagal farenheita peverskite į kelviną ir patikrinkite ar sutampa su ankstesniais skaičiavimais (išveskite true/false)
               - Nupieškite termometrą pagal Celsijų 
                 a) Atvaizduokite skalę, sugraduotą kas 5 laipsnius C priklausomai nuo įvestos temperatūros pridedant ir atimant 40 laipsnių 
                   (tarkime įvesta buvo 10, tuomet skalė bus nuo -30 iki +50)
                 b) Grafiškai atvaizduokite įvestą temperatūros stulpelį. 
                    <HINT> naudokite .ToString(), palyginimo reliacinius operatorius (==, >, < ir t.t.) ir .Replace(). 
                    if naudoti negalima, ternary operator (?) naudoti negalima.
            rezultatas gali atrodyti taip:
                                        |--------------------|
                                        |   ^F     _    ^C   |
                                        |  100  - | | -  40  |
                                        |   95  - | | -  35  |
                                        |   90  - | | -  30  |
                                        |   80  - | | -  25  |
                                        |   70  - | | -  20  |
                                        |   60  - | | -  15  |
                                        |   50  - |#| -  10  |
                                        |   40  - |#| -   5  |
                                        |   30  - |#| -   0  |
                                        |   20  - |#| -  -5  |
                                        |   10  - |#| - -10  |
                                        |    5  - |#| - -15  |
                                        |    0  - |#| - -20  |
                                        |  -10  - |#| - -25  |
                                        |  -20  - |#| - -30  |
                                        |  -30  - |#| - -35  |
                                        |  -40  - |#| - -40  |
                                        |        '***`       |
                                        |       (*****)      |
                                        |        `---'       |
                                        |____________________|
             */

            Console.Write("Iveskite temperatura celsijais:  ");
            var temperaturaCelsijais = double.Parse(Console.ReadLine());
            var temperaturaFarenheitais = temperaturaCelsijais * 9.0 / 5 + 32;
            Console.WriteLine("Temperatura Farenheitais: {0}", temperaturaFarenheitais);
            var temperaturaKelvinais = temperaturaCelsijais + 273.15;
            Console.WriteLine("Temperatura Kelvinais: {0}", temperaturaKelvinais);
            var temperaturaCelsijais2 = (temperaturaFarenheitais - 32) / 1.8;
            Console.WriteLine("Ar {0} sutampa su {1}? Atsakymas: {2}",
                temperaturaCelsijais, temperaturaCelsijais2, temperaturaCelsijais == temperaturaCelsijais2);
            var temperaturaCelsijais3 = temperaturaKelvinais - 273.15;
            Console.WriteLine("Ar {0} sutampa su {1}? Atsakymas: {2}",
                temperaturaCelsijais, temperaturaCelsijais3, temperaturaCelsijais == temperaturaCelsijais3);
            var temperaturaKelvinais2 = (temperaturaFarenheitais - 32) / 1.8 + 273.15;
            Console.WriteLine("Ar {0} sutampa su {1}? Atsakymas: {2}",
                temperaturaKelvinais, temperaturaKelvinais2, temperaturaKelvinais == temperaturaKelvinais2);
            Console.WriteLine();

            int gradavimas = (int)temperaturaCelsijais - (int)(temperaturaCelsijais % 5) + (int)(temperaturaCelsijais % 5 / 5 + 0.5) * 5;
            int skaleMax = gradavimas + 40;
            int skaleMin = gradavimas - 40;
            var i = skaleMax;
            Console.WriteLine("|---------------------|");
            Console.WriteLine("|    ^F    _    ^C    |");
            while (i > skaleMin)
            {
                var tempF = i * 9.0 / 5 + 32;
                var a = $"      {tempF} - ";
                var b = $" - {i}      ";
                var rodmuoF = a.Substring(a.Length - 10, 9);
                var rodmuoC = b.Substring(0, 9);
                var stulpelis = (gradavimas >= i).ToString()[0].ToString().Replace('T', '#').Replace('F', ' ');
                Console.WriteLine("|{0}|{2}|{1}|", rodmuoF, rodmuoC, stulpelis);
                i -= 5;
            }
            Console.WriteLine("|        '***`        |");
            Console.WriteLine("|       (*****)       |");
            Console.WriteLine("|        `---'        |");
            Console.WriteLine("|_____________________|");
        }
    }
}
