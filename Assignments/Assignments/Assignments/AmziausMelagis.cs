namespace Assignments06.Assignments
{
    internal class AmziausMelagis
    {
        public void PateiktiRegistracijosForma()
        {
            /*Sukurkite programą, kuri pateiktų vartotojo registracijos formą.
             Vartotojas įveda:
              - vardą ir pavardę
              - asmens kodą (11simb.)
              - amžių (sveiką skaičių metais) ir/arba gimimo datą (galima abu, tik kažkurį vieną, ar neįvesti nei vieno)
            Programa į ekraną išveda ataskatą:
             - šiandienos datą
             - Vardas, pavardė
             - Lytis
                <HINT> asmens kodo pirmasis rodo gimimo šimtmetį ir asmens lytį
                (1 – XIX a. gimęs vyras,
                 2 – XIX a. gimusi moteris,
                 3 – XX a. gimęs vyras,
                 4 – XX a. gimusi moteris,
                 5 – XXI a. gimęs vyras,
                 6 – XXI a. gimusi moteris
                 tolesni šeši:
                      metai (du skaitmenys),
                      mėnuo (du skaitmenys),
                      diena (du skaitmenys))    
             - Asmens kodas
                <NEPRIVALOMAS PASUNKINIMAS> asmens kodas validuojamas pagal tokias salygas
                   Paskaičiuojamas Kontrolinis skaičius
                   a) jei kontrolinis skaičius teisingas išvedamas asmens kodas
                   b) jei neteisingas išvedamas tekstas "kodas neteisingas",
                      o laukeAmžiaus patikimumas išvedama "patikimumui trūksta duomenų"
                      <HINT> https://lt.wikipedia.org/wiki/Asmens_kodas
             - Amžius
             - Amžiaus patikimumas - pagal tokias sąlygas:
             -  jei įvestas amžius metais, paskaičiuoti gimimo metus ir sulyginti su įvestu asmens kodu.
                a) jei sutampa išvesti "amžius patikimas"
                b) jei nesutampa išvesti "amžius pameluotas"
             - jei įvesta gimimo data sulyginti su įvestu asmens kodu.
                a) jei sutampa išvesti "amžius patikimas"
                b) jei nesutampa išvesti "amžius pameluotas"
             - jei įvesta ir amžius ir gimimo data sulyginti su įvestu asmens kodu.
                a) jei viskas sutampa išvesti "amžius tikras"
                b) jei asmens kodu sutampa tik vienas įvestų, išvesti "amžius nepatikimas"
                c) jei nesutampa išvesti "amžius pameluotas"
             - jei neįvesta nei amžius nei gimimo data išvesti
                a) "patikimumui trūksta duomenų"
     

                Rezultatas gali atrodyti taip:
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ ATASKAITA APIE ASMENĮ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓      2022-06-20       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓     Vardas, pavardė ▓ Vardenis Pavardenis                 ▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓               Lytis ▓ Vyras                               ▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓        Asmens kodas ▓ 44012029286                         ▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓              Amžius ▓ 82                                  ▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓         Gimimo data ▓ 1980-06-20                          ▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓ Amžiaus patikimumas ▓ amžius nepatikimas                  ▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓*/

            Console.Write("Iveskite varda ir pavarde:   ");
            var vardasPavarde = Console.ReadLine();
            if (// is vis neivesta:
                string.IsNullOrEmpty(vardasPavarde) || 
                // ivestas tik vardas arba tik pavarde:
                !vardasPavarde.Contains(" "))
            {
                Console.WriteLine("Vardas ir pavarde privalomi");
                Environment.Exit(1);
            }

            Console.Write("Iveskite asmens koda:   ");
            var asmensKodas = Console.ReadLine();
            if (asmensKodas.Length != 11)
            {
                Console.WriteLine("Blogas asmens kodas");
                    Environment.Exit(1);
            }

            var lytis = GautiLyti(asmensKodas[0]);

            Console.Write("Iveskite amziu ir gimimo data (nebutina):   ");
            var amziusGimimoData = Console.ReadLine();

            var amzius = GautiAmziu(amziusGimimoData);
            var gimimoData = GautiGimimoData(amziusGimimoData);

            var amziausPatikimumas = GautiAmziausPatikimuma(amzius, gimimoData, ValiduotiAsmensKoda(asmensKodas));

            AtspausdintiRezultata(DateTime.Now.ToString("yyyy - MM - dd"),
                vardasPavarde,
                lytis,
                ValiduotiAsmensKoda(asmensKodas),
                amzius.ToString(),
                gimimoData?.ToString("yyyy - MM - dd"),
                amziausPatikimumas);
        }

        private string GautiLyti(char asmensKodoPirmasSkaitmuo)
        {
            var skaitmuo = int.Parse(asmensKodoPirmasSkaitmuo.ToString());
            var liekana = skaitmuo % 2;
            return liekana switch
            {
                0 => "Moteris",
                1 => "Vyras",
                _ => ""
            };
        }

        private static int? GautiAmziu(string amziusGimimoData)
        {
            var reiksme = amziusGimimoData.Contains(" ") ?
                amziusGimimoData.Substring(0, amziusGimimoData.IndexOf(" ")) :
                amziusGimimoData;
            var arAmzius = int.TryParse(reiksme, out var amzius);
            if (arAmzius)
                return amzius;

            if (!amziusGimimoData.Contains(" "))
                return null;

            reiksme = amziusGimimoData.Substring(amziusGimimoData.IndexOf(" "), amziusGimimoData.Length - amziusGimimoData.IndexOf(" "));
            arAmzius = int.TryParse(reiksme, out amzius);
            if (arAmzius)
                return amzius;
            
            return null;
        }

        private DateTime? GautiGimimoData(string amziusGimimoData)
        {
            var reiksme = amziusGimimoData.Contains(" ") ?
                amziusGimimoData.Substring(0, amziusGimimoData.IndexOf(" ")) :
                amziusGimimoData;
            var arGimimoData = DateTime.TryParse(reiksme, out var gimimoData);
            if (arGimimoData)
                return gimimoData;

            if (!amziusGimimoData.Contains(" "))
                return null;

            reiksme = amziusGimimoData.Substring(amziusGimimoData.IndexOf(" "), amziusGimimoData.Length - amziusGimimoData.IndexOf(" "));
            arGimimoData = DateTime.TryParse(reiksme, out gimimoData);
            if (arGimimoData)
                return gimimoData;

            return null;
        }

        private string GautiAmziausPatikimuma(int? amzius, DateTime? gimimoData, string asmensKodas)
        {
            if (asmensKodas == "kodas neteisingas")
                return "patikimumui trūksta duomenų";

            if (amzius == null && gimimoData == null)
                return "patikimumui trūksta duomenų";

            var gimimoDataIsAsmensKodo = DateTime.Parse(GautiDataIsAsmensKodo(asmensKodas));
            var amziusIsAsmensKodo = (DateTime.Now - gimimoDataIsAsmensKodo).Days / 365;
            if (gimimoData == null)
            {
                if (amzius == amziusIsAsmensKodo)
                    return "amžius patikimas";
                return "amžius pameluotas";
            }

            if (amzius == null)
            {
                if (gimimoDataIsAsmensKodo == gimimoData)
                    return "amžius patikimas";
                return "amžius pameluotas";
            }

            if (gimimoDataIsAsmensKodo == gimimoData)
            {
                if (amzius == amziusIsAsmensKodo)
                    return "amžius tikras";
                return "amžius nepatikimas";
            }

            if (amzius == amziusIsAsmensKodo)
                return "amžius nepatikimas";
            return "amžius pameluotas";
        }

        private string ValiduotiAsmensKoda(string asmensKodas)
        {
            var arTeisingaData = DateTime.TryParse(GautiDataIsAsmensKodo(asmensKodas), out _);
            if (!arTeisingaData)
                return "kodas neteisingas";

            var kontrolinisSkaicius = (int.Parse(asmensKodas[0].ToString()) * 1 + int.Parse(asmensKodas[1].ToString()) * 2 +
                int.Parse(asmensKodas[2].ToString()) * 3 + int.Parse(asmensKodas[3].ToString()) * 4 + int.Parse(asmensKodas[4].ToString()) * 5 +
                int.Parse(asmensKodas[5].ToString()) * 6 + int.Parse(asmensKodas[6].ToString()) * 7 + int.Parse(asmensKodas[7].ToString()) * 8 +
                int.Parse(asmensKodas[8].ToString()) * 9 + int.Parse(asmensKodas[9].ToString()) * 1) % 11;
            if (kontrolinisSkaicius == 10)
            {
                kontrolinisSkaicius = (int.Parse(asmensKodas[0].ToString()) * 3 + int.Parse(asmensKodas[1].ToString()) * 4 +
                int.Parse(asmensKodas[2].ToString()) * 5 + int.Parse(asmensKodas[3].ToString()) * 6 + int.Parse(asmensKodas[4].ToString()) * 7 +
                int.Parse(asmensKodas[5].ToString()) * 8 + int.Parse(asmensKodas[6].ToString()) * 9 + int.Parse(asmensKodas[7].ToString()) * 1 +
                int.Parse(asmensKodas[8].ToString()) * 2 + int.Parse(asmensKodas[9].ToString()) * 3) % 11;
            }

            if (kontrolinisSkaicius == 10)
                kontrolinisSkaicius = 0;

            if (int.Parse(asmensKodas[10].ToString()) != kontrolinisSkaicius)
                return "kodas neteisingas";

            return asmensKodas;
        }

        private string GautiDataIsAsmensKodo(string asmensKodas)
        {
            var pirmiDuMetuSkaitmenys = GautiPirmusDuMetuSkaitmenis(asmensKodas[0]);
            var metai = pirmiDuMetuSkaitmenys + asmensKodas.Substring(1, 2);
            var menuo = asmensKodas.Substring(3, 2);
            var diena = asmensKodas.Substring(5, 2);
            return $"{metai}-{menuo}-{diena}";
        }

        private string GautiPirmusDuMetuSkaitmenis(char asmensKodoPirmasSkaitmuo)
        {
            var skaitmuo = int.Parse(asmensKodoPirmasSkaitmuo.ToString());
            return skaitmuo switch
            {
                1 => "18",
                2 => "18",
                3 => "19",
                4 => "19",
                5 => "20",
                6 => "20",
                _ => ""
            };
        }

        private void AtspausdintiRezultata(string ataskaitosData, string vardasPavarde, string lytis, string asmensKodas,
            string amzius, string gimimoData, string amziausPatikimumas)
        {
            var eileBeTeksto = "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓" + Environment.NewLine;
            var ataskaitosEile = "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ ATASKAITA APIE ASMENĮ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓" + Environment.NewLine
                + "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓     " + ataskaitosData + "    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓" + Environment.NewLine;
            var vardasPavardeEile = "▓     Vardas, pavardė ▓ ";
            var eilesGalas = "▓" + Environment.NewLine;
            var lytisEile = "▓               Lytis ▓ ";
            var asmensKodasEile = "▓        Asmens kodas ▓ ";
            var amziusEile = "▓              Amžius ▓ ";
            var gimimoDataEile = "▓         Gimimo data ▓ ";
            var amziausPatikimumasEile = "▓ Amžiaus patikimumas ▓ ";

            Console.Write("{0}{1}{0}{0}" +
                "{2}{3,-36}{4}{0}" +
                "{5}{6,-36}{4}{0}" +
                "{7}{8,-36}{4}{0}" +
                "{9}{10,-36}{4}{0}" +
                "{11}{12,-36}{4}{0}" +
                "{13}{14,-36}{4}{0}{0}", 
                eileBeTeksto, ataskaitosEile, vardasPavardeEile, vardasPavarde, eilesGalas,
                lytisEile, lytis, asmensKodasEile, asmensKodas, amziusEile, amzius, gimimoDataEile, gimimoData,
                amziausPatikimumasEile, amziausPatikimumas);
        }
    }
}
