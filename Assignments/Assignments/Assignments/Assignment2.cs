namespace Assignments06.Assignments
{
    internal class Assignment2
    {
        public void Method()
        {

            /*
             PARAŠYTI PROGRAMĄ KURI PRAŠO ĮVESTI ATSTUMĄ (KILOMENTRAIS) TARP TAŠKŲ A IR B IR DVIEJŲ TRANSPORTO PRIEMONIŲ GREITĮ (KM/H). 
             VIENA TRANSPORTO PRIEMONĖS PRADEDA VAŽIUOTI IŠ A, KITA IŠ B.STARTUOJA VIENU METU.
              - PASKAIČIUOTI ATSTUMĄ NUO A IKI VIETOS KURIOJE TRASPORTO PRIEMONĖS SUTITIKS METRAIS. 
              - PASKAIČIUOTI LAIKĄ KADA TRASPORTO PRIEMONĖS SUSITIKS SEKUNDĖMIS. 
              - PASKAIČIUOTI LAIKĄ, KADA TRASPORTUO PRIEMONĖS PASIEKS GALIUTINIUS TAŠKUS MINUTĖMIS.
              - PASKAIČIUOTI KIEK GRAMŲ CO2 IŠSKYRĖ ABI TRASPORTO PIEMONĖS KARTU SUDĖJUS. CO2 NORMA YRA 95 g/km.
              - GRAFIŠKAI PAVAIZDUOTI KELIĄ NUO A IKI B SUSKIRSTYTĄ Į 20 LYGIŲ SEGMENTŲ (TARKIME ĮVESTAS ATSTUMAS YRA 100KM, TUOMENT TURĖSIME 20 SEGMENTU PO 5KM).  
                A) PARODYTI VISO KELIO ILGĮ KM
                B) PARODYTI SEGMENTO ILGĮ KM
                C) PARODYTI KURIAME SEGMENTE TRASPORTO PREMONĖS SUTIKS IR ATSTUMĄ IKI SUSITIKIMO (TAŠKAS V)
                D) PARODYTI ABIEJŲ TRANSPORTO PRIEMONIŲ VAŽIAVIMO TRUKMĘ
                REZULTATAS GALI ARTODYTI TAIP:
               viso 100 km
             |--------------------------------------------------------------------------------------------------------------------------------------------|
             0      5     10     15     20      25     30     35     40     45     50     55     60     65     70     75     80     85     90     95    100
             |      |      |      |      |       |      |      |      |      |      |      |      |      |      |      |      |      |      |      |      |
            _A______|______|______|______|___V___|______|______|______|______|______|______|______|______|______|______|______|______|______|______|______B_
             |-------------------------------|                                                                                                                                                        
               susitikimo vieta 23,1 km
               susitikimo laikas po 0,87 val.
             | >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   200 min >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>|
             |<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<   60 min <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< |
             */

            // default: km, h, g

            Console.Write("Iveskite atstuma tarp A ir B (km):    ");
            var atstumas = int.Parse(Console.ReadLine());
            Console.Write("Iveskite A priemones greiti (km/h):  ");
            var greitisA = int.Parse(Console.ReadLine());
            Console.Write("Iveskite B priemones greiti (km/h):  ");
            var greitisB = int.Parse(Console.ReadLine());
            var susitikimoLaikas = (double)atstumas / (greitisA + greitisB);
            var susitikimoVieta = greitisA * susitikimoLaikas;
            var minValandoje = 60;
            var laikasAMin = (double)atstumas / greitisA * minValandoje;
            var laikasBMin = (double)atstumas / greitisB * minValandoje;
            var cO2Norma = 92; //g/km
            var bendrasCO2 = atstumas * cO2Norma * 2;
            var segmentas = (double)atstumas / 20;
            var atkarpa = segmentas;
            Console.WriteLine(" viso {0} km", atstumas);
            Console.WriteLine(" bendras CO2 {0} g", bendrasCO2);
            var virsutineLinija = " |----------------------------------------------------------------------" +
                "---------------------------------------------------------------------|";
            Console.WriteLine(virsutineLinija);
            Console.WriteLine(" 0    {0,3:0}    {1,3:0}    {2,3:0}    {3,3:0}    {4,3:0}    {5,3:0}    {6,3:0}    " +
                "{7,3:0}    {8,3:0}    {9,3:0}    {10,3:0}    {11,3:0}    {12,3:0}    {13,3:0}    {14,3:0}    {15,3:0}    " +
                "{16,3:0}    {17,3:0}    {18,3:0}    {19,3:0}", atkarpa, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas
                , atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas
                , atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas
                , atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas, atkarpa += segmentas);
            Console.WriteLine(" |      |      |      |      |      |      |      |      |      |      |      " +
                "|      |      |      |      |      |      |      |      |      |");
            var susitikimoVietosLinija = "_A______|______|______|______|______|______|______|______|______|______|______" +
                "|______|______|______|______|______|______|______|______|______B_";
            var ilgis = susitikimoVietosLinija.Length - 2;
            var susitikimoTaskas = (int)(susitikimoVieta / atstumas * ilgis);
            susitikimoVietosLinija = susitikimoVietosLinija.Substring(0, susitikimoTaskas + 1) + "V"
                + susitikimoVietosLinija.Substring(susitikimoTaskas + 2, ilgis - susitikimoTaskas);
            Console.WriteLine(susitikimoVietosLinija);
            var susitikimoVietosLinija2 = virsutineLinija.Substring(0, susitikimoTaskas + 1) + "|";
            Console.WriteLine(susitikimoVietosLinija2);
            Console.WriteLine("   susitikimo vieta {0:0.0} km", susitikimoVieta);
            Console.WriteLine("   susitikimo laikas po {0:0.00} val.", susitikimoLaikas);
            Console.WriteLine(" | >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>   {0,3:0} min >>>>>>>>>>>>>>" +
                ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>|", laikasAMin);
            Console.WriteLine(" |<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<  {0,3:0} min <<<<<<<<<<<<<<<<" +
                "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< |", laikasBMin);
        }
    }
}
