using System;

namespace MercadoLibre.Mutant.Dna.Core.Entities
{
    public class DnaResult
    {
        public Guid DnaResultId { set; get; }
        public string[] DnaContent { set; get; }
        public bool IsMutant { set; get; }

    }
}
