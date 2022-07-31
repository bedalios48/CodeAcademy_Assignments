using Assignments07.Data;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Assignments07.Assignments
{
    public class Kartuves
    {
        public static List<char> panaudotosRaides = new List<char> {' '};
        public static List<char> zodzioRaides;
        public static List<string> panaudotiZodziai = new List<string>();
        public static bool arZodisAtspetas = false;
        static bool kartotiZaidima = true;
        public static Dictionary<string, string[]> zodziuTemos;
        static int zodziuKiekis;
        static int bandymuKiekis = 7;

        public void Initialize()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("kartuves.json").Build();
            var section = config.GetSection(nameof(KartuvesData));
            var data = section.Get<KartuvesData>();
            zodziuTemos = data.ZodziuTemos;
            foreach (var item in zodziuTemos)
                zodziuKiekis += item.Value.Length;
            Console.OutputEncoding = Encoding.GetEncoding(1200);
            Console.InputEncoding = Encoding.GetEncoding(1200);
        }
        public void KartuviuVaizdavimas()
        {
            Initialize();
            while(kartotiZaidima)
            {
                Console.Clear();
                NaujasZaidimas();
                Console.WriteLine(TemuMeniu());
                string zodis;
                while(!IsrinktiZodi(zodziuTemos[PasirinktiMeniuPunkta()], out zodis))
                {
                    Console.WriteLine("Pasirinktoje kategorijoje žodžių nebeliko. Rinkitės kitą kategoriją.");
                }
                zodzioRaides = zodis.ToLower().ToCharArray().ToList();
                SpetiZodi(zodis);
                panaudotiZodziai.Add(zodis);
                Console.WriteLine("Ar žaisti dar kartą? T/N");
                ZaidimoKartojimas();
            }
        }

        public static void NaujasZaidimas()
        {
            arZodisAtspetas = false;
            panaudotosRaides = new List<char> { ' ' };
        }

        public static string TemuMeniu()
        {
            var temuMeniu = new StringBuilder();
            int i = 1;
            foreach (var tema in zodziuTemos)
            {
                temuMeniu.Append(i + ". " + tema.Key.ToUpper() + Environment.NewLine);
                i++;
            }
            return temuMeniu.ToString();
        }

        private string PasirinktiMeniuPunkta()
        {
            Console.WriteLine("Pasirinkite variantą:");
            int punktas;
            while (!int.TryParse(Console.ReadLine(), out punktas) || punktas < 1 || punktas > zodziuTemos.Count)
                Console.WriteLine("Blogas pasirinkimas. Rinkitės dar kartą:");
            return zodziuTemos.ElementAt(punktas - 1).Key;
        }

        public static bool IsrinktiZodi(string[] zodziai, out string? zodis)
        {
            var tinkamiZodziai = zodziai.ToList();
            tinkamiZodziai.RemoveAll(x => panaudotiZodziai.Contains(x));
            if (tinkamiZodziai.Count == 0)
            {
                zodis = null;
                return false;
            }
            var random = new Random();
            var indeksas = random.Next(tinkamiZodziai.Count);
            zodis = tinkamiZodziai[indeksas];
            return true;
        }

        private void SpetiZodi(string zodis)
        {
            var neteisingiSpejimai = 0;
            while (!arZodisAtspetas && neteisingiSpejimai < bandymuKiekis)
            {
                ZaidimoKonsole(neteisingiSpejimai);
                var raide = SpejimoPriemimas(out var zodzioSpejimas);
                if(zodzioSpejimas != null)
                {
                    if (zodzioSpejimas.ToLower() == zodis.ToLower())
                        arZodisAtspetas = true;
                    else neteisingiSpejimai = bandymuKiekis;
                    continue;
                }
                if (!zodzioRaides.Contains(raide) && !panaudotosRaides.Contains(raide))
                    neteisingiSpejimai++;
                if (!panaudotosRaides.Contains(raide))
                    panaudotosRaides.Add(raide);
                PatikrintiArZodisAtspetas();
            }

            ZaidimoKonsole(neteisingiSpejimai);
            Console.WriteLine();

            if (!arZodisAtspetas)
                Console.WriteLine("Neatspėjote! Žodis yra: {0}", zodis.ToUpper());
            else
                Console.WriteLine("Sveikiname! Atspėjote. Žodis yra: {0}", zodis.ToUpper());
        }

        private void ZaidimoKartojimas()
        {
            var pasirinkimas = Console.ReadKey().KeyChar;
            var galimiPasirinkimai = new string[] { "T", "N" };
            while (!galimiPasirinkimai.Contains(pasirinkimas.ToString().ToUpper()))
                pasirinkimas = Console.ReadKey().KeyChar;
            if (pasirinkimas.ToString().ToUpper() == "N")
                kartotiZaidima = false;
            if (panaudotiZodziai.Count == zodziuKiekis)
            {
                Console.WriteLine("Žaidimas baigtas! Visi žodžiai išnaudoti.");
                kartotiZaidima = false;
            }
        }

        private void ZaidimoKonsole(int neteisingiSpejimai)
        {
            Console.Clear();
            NupiestiKartuves(neteisingiSpejimai);
            Console.WriteLine("Bandytos raidės: {0}", string.Join(", ", GrazintiKlaidingasRaides()));
            Console.WriteLine(GrazintiSpejamaZodi());
            Console.Write("Spėkite raidę:  ");
        }

        public static List<char> GrazintiKlaidingasRaides()
        {
            var klaidingosRaides = new List<char>();
            foreach (var raide in panaudotosRaides)
                if (!zodzioRaides.Contains(raide))
                    klaidingosRaides.Add(raide);
            klaidingosRaides.Remove(' ');
            return klaidingosRaides;
        }

        private char RaidesPriemimas()
        {
            var tinkamosRaides = new char[] { 'a', 'ą', 'b', 'c', 'č', 'd', 'e', 'ę', 'ė', 'f', 'g',
                'h', 'i', 'į', 'y', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 'š', 't', 'u', 'ū',
                'ų', 'v', 'z', 'ž' };
            var raide = Console.ReadKey().KeyChar;
            while (!tinkamosRaides.Contains(char.Parse(raide.ToString().ToLower())))
                raide = Console.ReadKey().KeyChar;
            return raide;
        }

        private char SpejimoPriemimas(out string? zodis)
        {
            var tinkamosRaides = new char[] { 'a', 'ą', 'b', 'c', 'č', 'd', 'e', 'ę', 'ė', 'f', 'g',
                'h', 'i', 'į', 'y', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 'š', 't', 'u', 'ū',
                'ų', 'v', 'z', 'ž' };

            var arRaide = true;
            var arZodis = false;
            string ivestis;
            char raide;
            do
            {
                ivestis = Console.ReadLine();
                arRaide = char.TryParse(ivestis, out raide) && tinkamosRaides.Contains(raide);
                arZodis = ivestis.Length > 1;
            }
            while (!(arRaide || arZodis));

            zodis = arZodis ? ivestis.Trim() : null;

            return raide;
        }

        public static void PatikrintiArZodisAtspetas()
        {
            foreach(var raide in zodzioRaides)
            {
                if (!panaudotosRaides.Contains(raide))
                    return;
            }
            arZodisAtspetas = true;
        }

        private void NupiestiKartuves(int neteisingiSpejimai)
        {
            var zmogeliukas = new string[] { neteisingiSpejimai > 0 ? "O" : " ",
            neteisingiSpejimai > 1 ? "|" : " ",
            neteisingiSpejimai > 2 ? "/" : " ",
            neteisingiSpejimai > 3 ? "\\" : " ",
            neteisingiSpejimai > 4 ? "0" : " ",
            neteisingiSpejimai > 5 ? "\\" : " ",
            neteisingiSpejimai > 6 ? "/" : " "};

            Console.WriteLine("   - - - - - - |   ");
            Console.WriteLine("|              {0}", zmogeliukas[0]);
            Console.WriteLine("|             {0}{1}{2}", zmogeliukas[3], zmogeliukas[1], zmogeliukas[2]);
            Console.WriteLine("|              {0}", zmogeliukas[4]);
            Console.WriteLine("|             {0} {1}", zmogeliukas[6], zmogeliukas[5]);
            Console.WriteLine("|");
            Console.WriteLine("_ _ _ _");
            Console.WriteLine();
        }

        public static string GrazintiSpejamaZodi()
        {
            var zodzioStatymas = new StringBuilder();
            foreach(var zodzioRaide in zodzioRaides)
            {
                if (panaudotosRaides.Contains(zodzioRaide))
                {
                    zodzioStatymas.Append($"{zodzioRaide} ");
                    continue;
                }
                zodzioStatymas.Append("_ ");
            }
            return zodzioStatymas.ToString().Trim();
        }
    }
}
