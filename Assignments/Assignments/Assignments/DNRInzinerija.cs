namespace Assignments06.Assignments
{
    public class DNRInzinerija
    {
        private bool _arNormalizuota = false;
        private bool _arValiduota = false;
        public void ModifikuotiDNRGrandine()
        {
            
            /*Tarkime turime DNR grandinę užkoduotą tekstu var txt =" T CG-TAC- gaC-TAC-CGT-CAG-ACT-TAa-CcA-GTC-cAt-AGA-GCT    ".
            Galimos bazės: Adenine, Thymine, Cytosine, Guanine
                Parašykite programą kurioje atsiranda MENIU kuriame naudotojas gali pasirinkti:
                1. Atlikti DNR grandinės normalizavimo veiksmus:
                    - pašalina tarpus.
                    - visas raides keičia didžiosiomis.
                2. Atlikti grandinės validaciją
                    - patikrina ar nėra kitų nei ATCG raidžių
                3. Atlieka veiksmus su DNR grandine (tik tuo atveju jei grandinė yra normalizuota ir validi). Nuspaudus 3 įeinama į sub-meniu
                    - Jeigu grandinė yra validi, tačiau nenormalizuota programa pasiūlo naudotojui
                    1) normalizuoti grandinę
                    2) išeiti iš programos
                    - jei grandinė normalizuota arba kai buvo atlikta normalizacija
                    1) GCT pakeis į AGG
                    2) Išvesti ar yra tekste CAT
                    3) Išvesti trečią ir penktą grandinės segmentus (naudoti Substring()).
                    4) Išvesti raidžių kiekį tekste (naudoti string composition)
                    5) Išvesti ar yra tekste ir kiek kartų pasikartoja iš klaviatūros įvestas segmento kodas
                    6) Prie grandinės galo pridėti iš klaviatūros įvesta segmentą  
                        (reikalinga validacija ar nėra kitų kaip ATCG ir 3 raidės)
                    7) Iš grandinės pašalinti pasirinką elementą  
                    8) Pakeisti pasirinkti segmentą įvestu iš klaviatūros  
                        (reikalinga validacija ar nėra kitų kaip ATCG ir 3 raidės)
                    9) Grįžti į ankstesnį meniu
            Visoms operacijoms reikalingi testai.*/

            var grandine = " T CG-TAC- gaC-TAC-CGT-CAG-ACT-TAa-CcA-GTC-cAt-AGA-GCT    ";
            PagrindinisMeniu(ref grandine);
        }

        private void PagrindinisMeniu(ref string grandine)
        {
            var meniuPunktai = "1. Atlikti DNR grandinės normalizavimo veiksmus" + Environment.NewLine
                + "2. Atlikti grandinės validaciją" + Environment.NewLine
                + "3. Atlikti veiksmus su DNR grandine" + Environment.NewLine;
            var kurisMeniu = 1;

            while (true)
            {
                IsvestiMeniu(meniuPunktai);
                NukreiptiPagalPasirinkima(ref grandine, kurisMeniu);
            }
        }

        private void IsvestiMeniu(string meniuPunktai)
        {
            //Console.InputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.WriteLine(
                "Pasirinkite punktą iš meniu:" + Environment.NewLine
                + meniuPunktai
                + "Jei norite baigti, paspauskite ENTER");
        }

        private void NukreiptiPagalPasirinkima(ref string grandine, int kurisMeniu)
        {
            var input = Console.ReadLine();
            if(string.IsNullOrEmpty(input))
                Environment.Exit(1);
            if(!int.TryParse(input.Trim('.').Trim(')'), out var pasirinktasPunktas))
            {
                Console.WriteLine("Įvesta bloga vertė. Bandykite dar kartą.");
                NukreiptiPagalPasirinkima(ref grandine, kurisMeniu);
            }

            switch(kurisMeniu)
            {
                case 1:
                    NukreiptiIPasirinktaMetoda1(pasirinktasPunktas, ref grandine);
                    break;
                case 2:
                    NukreiptiIPasirinktaMetoda2(pasirinktasPunktas, ref grandine);
                    break;
                case 3:
                    NukreiptiIPasirinktaMetoda3(pasirinktasPunktas, ref grandine);
                    break;
            }
        }

        private void NukreiptiIPasirinktaMetoda1(int pasirinktasPunktas, ref string grandine)
        {
            switch (pasirinktasPunktas)
            {
                case 1:
                    grandine = AtliktiGrandinesNormalizavima(grandine);
                    Console.ReadLine();
                    break;
                case 2:
                    AtliktiGrandinesValidacija(grandine);
                    break;
                case 3:
                    AtliktiVeiksmusSuGrandine(ref grandine);
                    break;
                default:
                    Console.WriteLine("Pasirinktas meniu punktas neegzistuoja");
                    break;
            }
        }

        private void NukreiptiIPasirinktaMetoda2(int pasirinktasPunktas, ref string grandine)
        {
            switch (pasirinktasPunktas)
            {
                case 1:
                    grandine = AtliktiGrandinesNormalizavima(grandine);
                    Console.ReadLine();
                    AtliktiVeiksmusSuGrandine(ref grandine);
                    break;
                case 2:
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Pasirinktas meniu punktas neegzistuoja");
                    break;
            }
        }

        private void NukreiptiIPasirinktaMetoda3(int pasirinktasPunktas, ref string grandine)
        {
            switch (pasirinktasPunktas)
            {
                case 1:
                    {
                        grandine = GCTiAGG(grandine);
                        Console.ReadLine();
                    }
                    break;
                case 2:
                    {
                        if(ArTeksteYraCAT(grandine))
                        {
                            Console.WriteLine("Grandinėje yra \"CAT\". Paspauskite ENTER");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("Grandinėje nėra \"CAT\". Paspauskite ENTER");
                        Console.ReadLine();
                    }
                    break;
                case 3:
                    TreciasIrPenktasSegmentai(grandine);
                    break;
                case 4:
                    RaidziuKiekisTekste(grandine);
                    break;
                case 5:
                    SegmentoPaieska(grandine);
                    break;
                case 6:
                    grandine = PridetiSegmentaPrieGalo(grandine);
                    break;
                case 7:
                    grandine = PasalintiSegmenta(grandine);
                    break;
                case 8:
                    grandine = PakeistiSegmenta(grandine);
                    break;
                case 9:
                    PagrindinisMeniu(ref grandine);
                    break;
                default:
                    Console.WriteLine("Pasirinktas meniu punktas neegzistuoja");
                    break;
            }
        }

        private string PakeistiSegmenta(string grandine)
        {
            Console.WriteLine("Įveskite segmentą, kurį norite pakeisti:");
            var senasSegmentas = Console.ReadLine();
            Console.WriteLine("Įveskite naują segmentą:");
            var naujasSegmentas = Console.ReadLine();

            if (!ArValidusSegmentas(naujasSegmentas))
            {
                Console.WriteLine("Segmentas nėra teisingas. Paspauskite ENTER");
                Console.ReadLine();
                return grandine;
            }

            grandine = grandine.Replace(senasSegmentas, naujasSegmentas);
            Console.WriteLine("Segmentas pakeistas. Dabar grandinė: {0}. Paspauskite ENTER", grandine);
            Console.ReadLine();
            return grandine;
        }

        private string PasalintiSegmenta(string grandine)
        {
            Console.WriteLine("Įveskite segmentą, kurį norite pašalinti:");
            var segmentas = Console.ReadLine();
            grandine = grandine.Replace(segmentas, "").Replace("--", "-").Trim('-');
            Console.WriteLine("Segmentas pašalintas. Dabar grandinė: {0}. Paspauskite ENTER", grandine);
            Console.ReadLine();
            return grandine;
        }
        private string PridetiSegmentaPrieGalo(string grandine)
        {
            Console.WriteLine("Įveskite segmentą, kurį norite pridėti:");
            var segmentas = Console.ReadLine();
            if(!ArValidusSegmentas(segmentas))
            {
                Console.WriteLine("Blogai įvestas segmentas. Paspauskite ENTER");
                Console.ReadLine();
                return grandine;
            }
            grandine += "-" + segmentas;
            Console.WriteLine("Segmentas pridėtas. Dabar grandinė: {0}. Paspauskite ENTER", grandine);
            Console.ReadLine();
            return grandine;
        }

        public static bool ArValidusSegmentas(string segmentas)
        {
            if (segmentas.Length != 3)
                return false;

            if (segmentas.Replace("A", "")
                .Replace("T", "")
                .Replace("C", "")
                .Replace("G", "").Length != 0)
                return false;

            return true;
        }

        private void SegmentoPaieska(string grandine)
        {
            Console.WriteLine("Įveskite, kokio segmento norite ieškoti:");
            var segmentas = Console.ReadLine();
            if (grandine.Contains(segmentas))
            {
                Console.WriteLine("Segmentas rastas. Segmentas pasikartoja {0} kartą/us/ų",
                    grandine.Split(segmentas).Count() - 1);
                Console.WriteLine("Paspauskite ENTER");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Segmentas nerastas. Spauskite ENTER");
            Console.ReadLine();
        }

        private void RaidziuKiekisTekste(string grandine)
        {
            Console.WriteLine("Raidžių kiekis tekste yra: {0}",
                grandine.Replace("-", "").Length);
            Console.WriteLine("Paspauskite ENTER");
            Console.ReadLine();
        }

        private void TreciasIrPenktasSegmentai(string grandine)
        {
            var segmentai = grandine.Split('-');
            Console.WriteLine("Trecias segmentas yra {0}", segmentai[2]);
            Console.WriteLine("Penktas segmentas yra {0}", segmentai[4]);
            Console.WriteLine("Paspauskite ENTER");
            Console.ReadLine();
        }

        public static string GCTiAGG(string grandine)
        {
            grandine = grandine.Replace("GCT", "AGG");
            Console.WriteLine("GCT buvo pakeista į AGG. Dabar grandine: {0}. Paspauskite ENTER", grandine);
            return grandine;
        }

        public static bool ArTeksteYraCAT(string grandine)
        {
            if (grandine.Contains("CAT"))
                return true;
            return false;
        }

        public string AtliktiGrandinesNormalizavima(string grandine)
        {
            grandine = grandine.Replace(" ", "");
            grandine = grandine.ToUpper();
            _arNormalizuota = true;
            Console.WriteLine("Grandinė normalizuota. Paspauskite ENTER");
            return grandine;
        }

        private void AtliktiGrandinesValidacija(string validuojamaGrandine)
        {
            if (!ArValidiGrandine(validuojamaGrandine))
            {
                Console.WriteLine("Grandinė nėra validi. Paspauskite ENTER");
                Console.ReadLine();
                return;
            }
            _arValiduota = true;
            Console.WriteLine("Grandinė validuota. Paspauskite ENTER");
            Console.ReadLine();
        }

        public static bool ArValidiGrandine(string validuojamaGrandine)
        {
            validuojamaGrandine = validuojamaGrandine.ToUpper()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("A", "")
                .Replace("T", "")
                .Replace("C", "")
                .Replace("G", "");
            return string.IsNullOrEmpty(validuojamaGrandine);
        }

        private void AtliktiVeiksmusSuGrandine(ref string grandine)
        {
            if (!_arValiduota)
            {
                Console.WriteLine("Grandinė nėra validi, veiksmų atlikti negalima. Paspauskite ENTER");
                Console.ReadLine();
                return;
            }

            string? meniuPunktai;
            var kurisMeniu = 3;
            if (!_arNormalizuota)
            {
                Console.WriteLine("Grandinė nenormalizuota! Paspauskite ENTER");
                Console.ReadLine();
                meniuPunktai = "1) normalizuoti grandinę" + Environment.NewLine
                + "2) išeiti iš programos" + Environment.NewLine;
                kurisMeniu = 2;
            }
            else
            {
                meniuPunktai = "1) GCT pakeis į AGG" + Environment.NewLine
                        + "2) Išvesti ar yra tekste CAT" + Environment.NewLine
                        + "3) Išvesti trečią ir penktą grandinės segmentus" + Environment.NewLine
                        + "4) Išvesti raidžių kiekį tekste" + Environment.NewLine
                        + "5) Išvesti ar yra tekste ir kiek kartų pasikartoja iš klaviatūros įvestas segmento kodas" + Environment.NewLine
                        + "6) Prie grandinės galo pridėti iš klaviatūros įvesta segmentą  " + Environment.NewLine
                        + "7) Iš grandinės pašalinti pasirinką elementą  " + Environment.NewLine
                        + "8) Pakeisti pasirinktą segmentą įvestu iš klaviatūros" + Environment.NewLine
                        + "9) Grįžti į ankstesnį meniu" + Environment.NewLine;
            }

            IsvestiMeniu(meniuPunktai);
            NukreiptiPagalPasirinkima(ref grandine, kurisMeniu);
        }
    }
}
