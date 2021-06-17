using MercadoLibre.Mutant.Dna.Core.Services;
using MercadoLibre.Mutant.Dna.Test.FluentServices;
using MercadoLibre.Mutant.Dna.Test.Utils;
using System.Threading.Tasks;
using Xunit;

namespace MercadoLibre.Mutant.Dna.Test.DnaMutant
{
    public class DnaMutantTests
    {
        [Fact]
        public async Task TestValidDnaAsync()
        {
            //Arrange
            bool expectedResult = true;
            var mockDnaRepository = new MockDnaRepository().AddDnaResultSuccesfully();
            string[] validDna = LoadData.GetValidDna();
            //Act
            DnaServiceCore dnaServiceCore = new DnaServiceCore(mockDnaRepository.Object);
            bool result = await dnaServiceCore.IsMutant(validDna);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task TestInvalidDnaAsync()
        {
            //Arrange
            bool expectedResult = false;
            var mockDnaRepository = new MockDnaRepository().AddDnaResultSuccesfully();
            string[] validDna = LoadData.GetInValidDna();
            //Act
            DnaServiceCore dnaServiceCore = new DnaServiceCore(mockDnaRepository.Object);
            bool result = await dnaServiceCore.IsMutant(validDna);
            //Assert
            Assert.Equal(expectedResult, result);
        }


    }
}
