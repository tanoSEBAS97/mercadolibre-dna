using MercadoLibre.Mutant.Dna.Core.Entities;
using MercadoLibre.Mutant.Dna.Core.Repositories;
using Moq;

namespace MercadoLibre.Mutant.Dna.Test.FluentServices
{
    public class MockDnaRepository : Mock<IDnaRepository>
    {
        public MockDnaRepository AddDnaResultSuccesfully()
        {
            Setup(x => x.Add(It.IsAny<DnaResult>()));
            return this;
        }
    }
}
