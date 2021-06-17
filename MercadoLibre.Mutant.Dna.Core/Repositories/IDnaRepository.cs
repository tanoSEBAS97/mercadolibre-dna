using MercadoLibre.Mutant.Dna.Core.Entities;
using System.Threading.Tasks;

namespace MercadoLibre.Mutant.Dna.Core.Repositories
{
    public interface IDnaRepository
    {
        Task Add(DnaResult dnaResult);
        Task<DnaStats> GetStats();
    }
}
