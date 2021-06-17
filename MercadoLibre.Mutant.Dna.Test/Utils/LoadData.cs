using Newtonsoft.Json;
using System.IO;

namespace MercadoLibre.Mutant.Dna.Test.Utils
{
    public static class LoadData
    {
        public static string[] GetValidDna()
        {
            string[] dna;
            using (StreamReader streamReader = new StreamReader("./JsonFiles/ValidDna.json"))
            {
                string json = streamReader.ReadToEnd();
                dna = JsonConvert.DeserializeObject<string[]>(json);
            }
            return dna;
        }

        public static string[] GetInValidDna()
        {
            string[] dna;
            using (StreamReader streamReader = new StreamReader("./JsonFiles/InvalidDna.json"))
            {
                string json = streamReader.ReadToEnd();
                dna = JsonConvert.DeserializeObject<string[]>(json);
            }
            return dna;
        }

    }
}
