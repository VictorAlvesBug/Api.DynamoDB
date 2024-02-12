using Amazon.DynamoDBv2.Model;

namespace Api.DynamoDB.Application.Models.Indexes
{
	public class TableIndexesModel
	{
        public IEnumerable<GlobalSecondaryIndexDescription> GlobalSecondaryIndexes { get; set; }
        public IEnumerable<LocalSecondaryIndexDescription> LocalSecondaryIndexes { get; set; }
    }
}
