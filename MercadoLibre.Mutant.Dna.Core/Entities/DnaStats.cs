namespace MercadoLibre.Mutant.Dna.Core.Entities
{
    public class DnaStats
    {
        public long CountMutantDna { set; get; }
        public long CountHumanDna { set; get; }
        public double Ratio
        {
            get
            {
                return CountHumanDna == 0 ? 0 : (double)CountMutantDna / CountHumanDna;
            }
        }
    }
}
