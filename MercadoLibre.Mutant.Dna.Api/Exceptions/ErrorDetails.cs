namespace MercadoLibre.Mutant.Dna.Api.Exceptions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public ErrorDetails(string message)
        {
            Message = message;
        }
    }
}
