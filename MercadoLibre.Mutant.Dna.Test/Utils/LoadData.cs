using Newtonsoft.Json;
using System.IO;

namespace MercadoLibre.Mutant.Dna.Test.Utils
{
    public static class LoadData
    {
        public static string[] GetDna(string fileName)
        {
            string[] dna;
            using (StreamReader streamReader = new StreamReader($"./JsonFiles/{fileName}.json"))
            {
                string json = streamReader.ReadToEnd();
                dna = JsonConvert.DeserializeObject<string[]>(json);
            }
            return dna;
        }

    }
}
