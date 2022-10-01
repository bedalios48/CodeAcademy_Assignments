namespace Assignments09.Domain.Models.Concrete
{
    public class Stulpelis
    {
        public Stulpelis(int stulpelioNr, List<int> diskai)
        {
            StulpelioNr = stulpelioNr;
            Diskai = diskai;
        }

        public Stulpelis(int stulpelioNr)
        {
            StulpelioNr = stulpelioNr;
        }

        public int StulpelioNr { get; private set; }
        public List<int> Diskai { get; private set; } = new List<int>();

        public void PridetiDiska(int diskas)
        {
            Diskai.Add(diskas);
        }
    }
}
