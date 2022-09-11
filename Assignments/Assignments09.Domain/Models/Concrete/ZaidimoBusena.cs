namespace Assignments09.Domain.Models.Concrete
{
    public class ZaidimoBusena
    {
        public ZaidimoBusena()
        {
            Stulpeliai = new List<Stulpelis>
            {
                new Stulpelis(1, new List<int>{ 1, 2, 3, 4 }),
                new Stulpelis(2),
                new Stulpelis(3)
            };
            Paemimas = true;
            ArGeraiPadeta = true;
            ArGeraiPaimta = true;
            TurimoDiskoDydis = -1;
            StulpelioNr = 0;
            EjimoNr = 0;
            ArGeraIvestis = true;
            TxtLogas = new TxtLogas();
            Pagalba = false;
            RekomenduojamasEjimas = new int[] { 0, 0 };
            Iseiti = false;
            Statistika = false;
        }
        
        public List<Stulpelis> Stulpeliai { get; set; }
        public bool Paemimas { get; set; }
        public bool ArGeraiPaimta { get; set; }
        public bool ArGeraiPadeta { get; set; }
        public int TurimoDiskoDydis { get; set; }
        public int StulpelioNr { get; set; }
        public int EjimoNr { get; set; }
        public bool ArGeraIvestis { get; set; }
        public TxtLogas TxtLogas { get; set; }
        public bool Pagalba { get; set; }
        public int[] RekomenduojamasEjimas { get; set; }
        public bool Iseiti { get; set; }

        public bool PavykesEjimas
        {
            get { return Paemimas && ArGeraIvestis && ArGeraiPaimta && ArGeraiPadeta; }
        }

        public bool Statistika { get; set; }
    }
}
