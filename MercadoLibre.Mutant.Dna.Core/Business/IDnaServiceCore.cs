using MercadoLibre.Mutant.Dna.Core.Entities;
using System;
using System.Threading.Tasks;

namespace MercadoLibre.Mutant.Dna.Core.Services
{
    public interface IDnaServiceCore
    {
        public Task<bool> IsMutant(String[] dna);
        public Task<DnaStats> GetStats();
    }
}
