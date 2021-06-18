using MercadoLibre.Mutant.Dna.Core.Services;
using MercadoLibre.Mutant.Dna.Test.FluentServices;
using MercadoLibre.Mutant.Dna.Test.Utils;
using System.Threading.Tasks;
using Xunit;

namespace MercadoLibre.Mutant.Dna.Test.DnaMutant
{
    public class DnaMutantTests
    {
        public const string ValidDna = "ValidDna";
        public const string InvalidDna = "InvalidDna";
        public const string FullValidDna = "FullValidDna";


        [Fact]
        public async Task TestValidDnaAsync()
        {
            //Arrange
            bool expectedResult = true;
            var mockDnaRepository = new MockDnaRepository().AddDnaResultSuccesfully();
            string[] validDna = LoadData.GetDna(ValidDna);
            //Act
            DnaServiceCore dnaServiceCore = new DnaServiceCore(mockDnaRepository.Object);
            bool result = await dnaServiceCore.IsMutant(validDna);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task TestInValidDna()
        {
            //Arrange
            bool expectedResult = false;
            var mockDnaRepository = new MockDnaRepository().AddDnaResultSuccesfully();
            string[] validDna = LoadData.GetDna(InvalidDna);
            //Act
            DnaServiceCore dnaServiceCore = new DnaServiceCore(mockDnaRepository.Object);
            bool result = await dnaServiceCore.IsMutant(validDna);
            //Assert
            Assert.Equal(expectedResult, result);
        }

        //test all possible ways
        [Fact]
        public async Task TestFullValidDna()
        {
            //Arrange
            bool expectedResult = true;
            var mockDnaRepository = new MockDnaRepository().AddDnaResultSuccesfully();
            string[] validDna = LoadData.GetDna(FullValidDna);
            //Act
            DnaServiceCore dnaServiceCore = new DnaServiceCore(mockDnaRepository.Object);
            bool result = await dnaServiceCore.IsMutant(validDna);
            //Assert
            Assert.Equal(expectedResult, result);
        }




    }
}
