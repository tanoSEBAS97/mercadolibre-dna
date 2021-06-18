using System.ComponentModel.DataAnnotations;

namespace MercadoLibre.Mutant.Dna.Api.Requests
{
    public class IsMutant
    {
        [Required(ErrorMessage = "Invalid input")]
        public string[] dna { set; get; }
    }
}
