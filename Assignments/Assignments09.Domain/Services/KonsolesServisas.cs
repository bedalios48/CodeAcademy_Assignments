using Assignments09.Domain.Enums;
using Assignments09.Domain.Interfaces;
using Assignments09.Domain.Models.Concrete;

namespace Assignments09.Domain.Services
{
    public class KonsolesServisas : IKonsolesServisas
    {
        public void AtspausdintiZaidima(ZaidimoBusena zaidimoBusena)
        {
            var laukas = new string[3][]
            {
                new string[5],
                new string[5],
                new string[5]
            };
            for (int i = 0; i < zaidimoBusena.Stulpeliai.Count; i++)
            {
                zaidimoBusena.Stulpeliai[i].Diskai.Sort();
                var diskuKiekis = zaidimoBusena.Stulpeliai[i].Diskai.Count;
                for (int j = 4; j >= 0; j--)
                {
                    laukas[i][j] = diskuKiekis > 0 ? SukurtiDiska(zaidimoBusena.Stulpeliai[i].Diskai[diskuKiekis - 1]) : SukurtiDiska(0);
                    diskuKiekis--;
                }
            }
            var stulpeliuRodmenys = new string[4]
            {
                "       ",
                zaidimoBusena.StulpelioNr == 1 ? "--^^^[1]^^^--" : "-----[1]-----",
                zaidimoBusena.StulpelioNr == 2 ? "--^^^[2]^^^--" : "-----[2]-----",
                zaidimoBusena.StulpelioNr == 3 ? "--^^^[3]^^^---" : "-----[3]------"
            };
            Console.Clear();
            Console.WriteLine("Norėdami gauti pagalbą, spauskite \'H\'. Norėdami ištraukti statistiką, spauskite \'S\'.");
            Console.WriteLine($"Ejimas {zaidimoBusena.EjimoNr + 1}");

            var pagalbosEilute = new string[2] { "", "" };
            if (zaidimoBusena.Pagalba)
            {
                var pagalba = zaidimoBusena.RekomenduojamasEjimas;
                var pagalboeEilutesRodmenys = new string[4]
                    {
                    "       ",
                    pagalba[0] == 1 ? "    ↑↑↑↑↑    ": pagalba[1] == 1 ? "    ↓↓↓↓↓    " : "             ",
                    pagalba[0] == 2 ? "    ↑↑↑↑↑    ": pagalba[1] == 2 ? "    ↓↓↓↓↓    " : "             ",
                    pagalba[0] == 3 ? "    ↑↑↑↑↑    ": pagalba[1] == 3 ? "    ↓↓↓↓↓    " : "             ",
                    };
                pagalbosEilute[0] = string.Join("", pagalboeEilutesRodmenys);
                var isKurPaimti = (StulpelioNrKilmininkas)pagalba[0];
                var iKurPadeti = (StulpelioNrGalininkas)pagalba[1];
                pagalbosEilute[1] = $"paimkite diską iš {isKurPaimti} stulpelio ir padėkite į {iKurPadeti}";
            }
            Console.WriteLine();
            Console.WriteLine($"Diskas rankoje: {SukurtiDiska(zaidimoBusena.TurimoDiskoDydis)}");
            Console.WriteLine(pagalbosEilute[0]);
            Console.WriteLine(" 1eil. " + laukas[0][0] + laukas[1][0] + laukas[2][0]);
            Console.WriteLine(" 2eil. " + laukas[0][1] + laukas[1][1] + laukas[2][1]);
            Console.WriteLine(" 3eil. " + laukas[0][2] + laukas[1][2] + laukas[2][2]);
            Console.WriteLine(" 4eil. " + laukas[0][3] + laukas[1][3] + laukas[2][3]);
            Console.WriteLine(" 5eil. " + laukas[0][4] + laukas[1][4] + laukas[2][4]);
            Console.WriteLine(string.Join("", stulpeliuRodmenys));
            Console.WriteLine();
            if (!zaidimoBusena.ArGeraiPadeta)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NEGALIMA DIDESNIO DISKO DĖTI ANT MAŽESNIO");
                Console.ResetColor();
            }
            if (!zaidimoBusena.ArGeraiPaimta)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("STULPELYJE NĖRA DISKO");
                Console.ResetColor();
            }
            if (!zaidimoBusena.ArGeraIvestis)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NETEISINGA ĮVESTIS");
                Console.ResetColor();
            }
            if (zaidimoBusena.Pagalba)
            {
                if (zaidimoBusena.RekomenduojamasEjimas[0] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("PAGALBA NEGALIMA");
                    Console.ResetColor();
                }
                else
                    Console.WriteLine(pagalbosEilute[1]);
            }
            zaidimoBusena.Pagalba = false;
            if (zaidimoBusena.Paemimas)
                Console.Write("Pasirinkite stulpeli, is kurio paimti: ");
            else
                Console.Write("Pasirinkite stulpeli, i kuri padeti: ");
        }

        private string SukurtiDiska(int dydis)
        {
            return dydis switch
            {
                -1 => "",
                0 => "      |      ",
                1 => "     #|#     ",
                2 => "    ##|##    ",
                3 => "   ###|###   ",
                4 => "  ####|####  "
            };
        }

        public int IvestiesPriemimas(ZaidimoBusena zaidimoBusena)
        {
            var ivestis = Console.ReadKey();
            if (ivestis.Key == ConsoleKey.Escape)
            {
                zaidimoBusena.Iseiti = true;
                return 0;
            }
            if (ivestis.KeyChar.ToString().ToUpper() == "H" && zaidimoBusena.Paemimas)
            {
                zaidimoBusena.Pagalba = true;
                return 0;
            }
            if (ivestis.KeyChar.ToString().ToUpper() == "S")
            {
                zaidimoBusena.Statistika = true;
                return 0;
            }
            zaidimoBusena.ArGeraIvestis = int.TryParse(ivestis.KeyChar.ToString(),
                    out var stulpelioNr) && stulpelioNr >= 1 && stulpelioNr <= 3;
            return stulpelioNr;
        }

        public void PranestiApieBlogaKonfiguracija()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("BŪTINA PASIRINKTI BENT VIENĄ LOGINIMO BŪDĄ!");
            Console.ResetColor();
            Environment.Exit(0);
        }

        public void AtspausdintiStatistikosMeniu()
        {
            Console.WriteLine("Pasirinkite ataskaitos tipą:");
            Console.WriteLine("1. Ėjimų kiekis iki laimėjimo");
            Console.WriteLine("2. Perteklinių ėjimų kiekis");
        }

        public int PriimtiSkaiciu(int max)
        {
            int skaicius;
            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out skaicius) && skaicius > max)
                Console.WriteLine("Įvesta neteisingai! Veskite dar kartą:");
            return skaicius;
        }

        public void AtspausdintiStatistika(List<ZaidimoStatistika> zaidimuStatistikos, int ataskaitosTipas)
        {
            zaidimuStatistikos.Sort((x,y) => x.ZaidimoData.CompareTo(y.ZaidimoData));
            var trumpiausiasEjimuSkaicius = 15;
            
            Console.WriteLine("-------------------------------------------------------------- ");
            var meniu = ataskaitosTipas == 1 ?
                "| Žaidimo data       | Ėjimų kiekis iki laimėjimo | Pokytis  |" :
                "| Žaidimo data       | Perteklinių ėjimų kiekis   | Pokytis  |";
            Console.WriteLine(meniu);
            Console.WriteLine("-------------------------------------------------------------- ");
            var buvesZaidimas = 0;
            foreach(var zaidimoStatistika in zaidimuStatistikos)
            {
                var antrasStulpelis = zaidimoStatistika.Laimetas ?
                        ataskaitosTipas == 1 ? zaidimoStatistika.EjimuSkaicius.ToString() :
                        (zaidimoStatistika.EjimuSkaicius - trumpiausiasEjimuSkaicius).ToString()
                    : "N/B";
                var treciasStulpelis = zaidimoStatistika.Laimetas ?
                    buvesZaidimas == 0 ? "N/G" : (zaidimoStatistika.EjimuSkaicius - buvesZaidimas).ToString() : "N/G";
                Console.WriteLine($"|  {zaidimoStatistika.ZaidimoData}  |    {antrasStulpelis, -3}                     " +
                    $"|    {treciasStulpelis, -3}   |");
                Console.WriteLine("--------------------------------------------------------------");
                if(zaidimoStatistika.Laimetas)
                    buvesZaidimas = zaidimoStatistika.EjimuSkaicius;
            }
        }

        public bool ArTestiZaidima()
        {
            Console.WriteLine("Ar tęsti žaidimą? t/n");
            var ivestis = Console.ReadKey();
            while(!(ivestis.KeyChar.ToString().ToUpper() == "T" || ivestis.KeyChar.ToString().ToUpper() == "N"))
                ivestis = Console.ReadKey();
            if (ivestis.KeyChar.ToString().ToUpper() == "T")
                return true;
            return false;
        }
    }
}
