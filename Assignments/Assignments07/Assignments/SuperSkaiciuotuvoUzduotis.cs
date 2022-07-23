namespace Assignments07.Assignments
{
    public class SuperSkaiciuotuvoUzduotis
    {
        /*    ## Super Skaiciuotuvas ## 
             Sukurti skaiciuotuva.Ijungus programa turetume gauti pranešimą “
           1. Nauja operacija 2 Iseiti.

             Pasirinkus 1 vada į submeniu:
           1. Sudetis 2. Atimtis 3. Daugyba 4. Dalyba

             Pasirinkus viena is operaciju programa turetu paprasyti naudotoja ivesti pirma ir antra skaicius,
             o gale isvesti rezultata į ekraną. Po rezultato parodymo naudotojui parodomas submeniu su klausimu

               ar naudotojas nori atlikti nauja operacija ar testi su rezultatu. 
           1. Nauja operacija 2. Testi su rezultatu 3. Iseiti”

             Pasirinkus 2 programa turetu paprasyti ivesti kokia operacija turetu buti atliekama ir paprasyti
               TIK SEKANCIO SKAITMENS. 

             Pasirinkus 3 programa turetu issijungti.Visa kita turetu veikti tokiu pat budu.

             Pvz:
           > 1. Nauja operacija 2 Iseiti.
             _1
             > 1. Sudetis 2. Atimtis 3. Daugyba 4. Dalyba
             _1
           > pasirinktas veiksmas + 
           > iveskite pirma skaiciu
             _15
           > iveskite antra skaiciu
             _45
           > Rezultatas: 60
           > 1. Nauja operacija 2. Testi su rezultatu 3. Iseiti
             _2
           > 1. Sudetis 2. Atimtis 3. Daugyba 4. Dalyba
             _2
           > pasirinktas veiksmas - 
           > Iveskite skaiciu

             _10
             > Rezultatas: 50
           > 1. Nauja operacija 2. Testi su rezultatu 3. Iseiti
             _1
           > 1. Sudetis 2. Atimtis 3. Daugyba 4. Dalyba
             _2
           > pasirinktas veiksmas * 
             > iveskite pirma skaiciu
             _2
           > iveskite antra skaiciu
             _3
           > Rezultatas: 6
           > 1. Nauja operacija 2. Testi su rezultatu 3. Iseiti
             _3
           > Baigta




             BONUS1: Iskelkite operacijas i metodus

             BONUS2: Parasykite operacijoms validacijas pries ivestus neteisingus skaicius.
                     - dalyba is nulio, neteisingu ivesciu prevencija
                     - kada tikimasi gauti skaiciu, bet gaunamas char arba string.
                   - neteisingas meniu punkto pasirinkimas

             BONUS3: Parasyti unit testus uztikrinant operaciju veikima

             BONUS4: Parasyti laipsnio pakelimo ir saknies traukimo operacijas
                 */

        static double? rezultatas = null;
        static bool pagrindinisMeniu = true;
        static bool meniuBusena = true;
        static bool ijungta = true;
        static int operacija;

        public void SkaiciuotuvoMeniu()
        {
            while(ijungta)
            {
                if (meniuBusena && pagrindinisMeniu)
                {
                    Console.Clear();
                    Console.WriteLine("Rezultatas: {0}", Rezultatas());
                }
                Console.WriteLine(PaprasytiSkaiciaus());
                var punktas = GautiIvestaReiksme();
                SuperSkaiciuotuvas(punktas);
            }
        }

        private static string GautiIvestaReiksme()
        {
            if(meniuBusena)
                return GautiMeniuPunkta();
            return GautiSkaiciu();
        }

        private static string GautiSkaiciu()
        {
            var skaicius = GautiIvestaSkaiciu();
            while(skaicius == 0 && (operacija == 4 || operacija == 5)
                && rezultatas != null)
            {
                Console.WriteLine("0 siame veiksme negalimas!");
                skaicius = GautiIvestaSkaiciu();
            }

            return skaicius.ToString();
        }

        private static double GautiIvestaSkaiciu()
        {
            double skaicius;
            while (!double.TryParse(Console.ReadLine(), out skaicius))
                Console.WriteLine("Blogai ivestas skaicius. Veskite dar karta:");
            return skaicius;
        }

        private static string GautiMeniuPunkta()
        {
            int punktas;
            while (!int.TryParse(Console.ReadLine(), out punktas)
                || !GautiTinkamusMeniuPunktus().Contains(punktas))
                Console.WriteLine("Blogai ivestas skaicius. Veskite dar karta:");

            return punktas.ToString();
        }

        public static List<int> GautiTinkamusMeniuPunktus()
        {
            if (pagrindinisMeniu)
            {
                return rezultatas == null ?
                    new List<int> { 1, 2 } : new List<int> { 1, 2, 3 };
            }
            return new List<int> { 1, 2, 3, 4, 5, 6 };
        }

        public static string PaprasytiSkaiciaus()
        {
            if (meniuBusena)
                return IsvestiMeniu();
            if (rezultatas == null)
                return "Iveskite pirma skaiciu:";
            return "Iveskite antra skaiciu:";
        }

        public static void SuperSkaiciuotuvas(string ivedimas)
        {
            if(meniuBusena)
            {
                if(pagrindinisMeniu)
                {
                    PagrindinioMeniuApdorojimas(ivedimas);
                    return;
                }
                operacija = int.Parse(ivedimas);
                meniuBusena = false;
                pagrindinisMeniu = true;
                return;
            }

            if (rezultatas == null)
            {
                rezultatas = double.Parse(ivedimas);
                return;
            }

            meniuBusena = true;
            rezultatas = AtliktiOperacija(double.Parse(ivedimas));
        }

        private static void PagrindinioMeniuApdorojimas(string ivedimas)
        {
            if (ivedimas == "1")
                rezultatas = null;
            if ((ivedimas == "2" && rezultatas == null) || ivedimas == "3")
                ijungta = false;
            pagrindinisMeniu = false;
        }

        public static double AtliktiOperacija(double antrasSkaicius)
        {
            return operacija switch
            {
                1 => Sudetis((double)rezultatas, antrasSkaicius),
                2 => Atimtis((double)rezultatas, antrasSkaicius),
                3 => Daugyba((double)rezultatas, antrasSkaicius),
                4 => Dalyba((double)rezultatas, antrasSkaicius),
                5 => SakniesTraukimas((double)rezultatas, antrasSkaicius),
                6 => LaipsnioKelimas((double)rezultatas, antrasSkaicius)
            };
        }

        public static double Sudetis(double pirmas, double antras) => pirmas + antras;
        public static double Atimtis(double pirmas, double antras) => pirmas - antras;
        public static double Daugyba(double pirmas, double antras) => pirmas * antras;
        public static double Dalyba(double pirmas, double antras) => pirmas / antras;
        public static double SakniesTraukimas(double pirmas, double antras) => Math.Pow(pirmas, 1 / antras);
        public static double LaipsnioKelimas(double pirmas, double antras) => Math.Pow(pirmas, antras);

        public static double Rezultatas()
        {
            return rezultatas ?? 0;
        }
        public static void Reset()
        {
            rezultatas = null;
            pagrindinisMeniu = true;
            meniuBusena = true;
            ijungta = true;
        }

        public static string IsvestiMeniu()
        {
            if(pagrindinisMeniu)
                return rezultatas == null ?
                "1. Nauja operacija" + Environment.NewLine + "2 Iseiti" :
                "1.Nauja operacija" + Environment.NewLine + "2.Testi su rezultatu"
                + Environment.NewLine + "3.Iseiti";
            return "1. Sudetis" + Environment.NewLine +
                "2. Atimtis" + Environment.NewLine +
                "3. Daugyba" + Environment.NewLine + 
                "4. Dalyba" + Environment.NewLine + 
                "5. Saknies traukimas" + Environment.NewLine + 
                "6. Kelimas laipsniu";
        }
    }
}
