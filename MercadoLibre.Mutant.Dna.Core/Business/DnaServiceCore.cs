using MercadoLibre.Mutant.Dna.Core.Entities;
using MercadoLibre.Mutant.Dna.Core.Repositories;
using MercadoLibre.Mutant.Dna.Core.Util;
using System;
using System.Threading.Tasks;

namespace MercadoLibre.Mutant.Dna.Core.Services
{
    public class DnaServiceCore : IDnaServiceCore
    {
        private readonly IDnaRepository _dnaRepository;

        public DnaServiceCore(IDnaRepository dnaRepository)
        {
            _dnaRepository = dnaRepository;
        }

        public async Task<DnaStats> GetStats()
        {
            return await _dnaRepository.GetStats();
        }

        public async Task<bool> IsMutant(String[] dna)
        {
            bool isMutant = DnaMutantUtil.IsMutant(dna);
            DnaResult dnaResult = new DnaResult
            {
                DnaResultId = Guid.NewGuid(),
                DnaContent = dna,
                IsMutant = isMutant
            };
            await _dnaRepository.Add(dnaResult);
            return isMutant;
        }
    }
}
