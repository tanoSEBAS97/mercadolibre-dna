using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using MercadoLibre.Mutant.Dna.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoLibre.Mutant.Dna.Core.Repositories
{
    public class DnaDynamoRepository : IDnaRepository
    {
        private const string TableName = "DnaResults";
        private const string DnaResultId = "DnaResultId";
        private const string DnaContent = "DnaContent";
        private const string IsMutant = "IsMutant";
        private readonly IAmazonDynamoDB _amazonDynamoDB;

        public DnaDynamoRepository(IAmazonDynamoDB amazonDynamoDB)
        {
            _amazonDynamoDB = amazonDynamoDB;
        }

        public async Task Add(DnaResult newEntity)
        {
            PutItemRequest putItemRequest = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>()
                {
                    {DnaResultId,new AttributeValue{ S=newEntity.DnaResultId.ToString()}  },
                    {DnaContent,new AttributeValue{ S=string.Join(",",newEntity.DnaContent)} },
                    {IsMutant,new AttributeValue{ BOOL=newEntity.IsMutant} }
                }
            };
            await _amazonDynamoDB.PutItemAsync(putItemRequest);
        }

        public async Task<DnaStats> GetStats()
        {
            long allrecords = 0;
            long mutants = 0;
            Dictionary<string, AttributeValue> lastKeyEvaluated;
            do
            {
                var request = new ScanRequest
                {
                    TableName = TableName,
                    ConsistentRead = true,
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":IsMutant", new AttributeValue { BOOL=true }} },
                    FilterExpression = "IsMutant=:IsMutant",
                    Select = Select.COUNT
                };
                var response = await _amazonDynamoDB.ScanAsync(request);
                allrecords = allrecords + response.ScannedCount;
                mutants = mutants + response.Count;
                lastKeyEvaluated = response.LastEvaluatedKey;
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);

            DnaStats dnaStats = new DnaStats
            {
                CountHumanDna = allrecords,
                CountMutantDna = mutants
            };
            return dnaStats;
        }
    }
}
