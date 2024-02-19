using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace Api.DynamoDB.Infrastructure.Utils
{
	public static class DynamoUtils
	{
		public static AmazonDynamoDBClient GetClient()
		{
			var _awsAccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
			var _awsSecretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");

			var config = new AmazonDynamoDBConfig
			{
				RegionEndpoint = Amazon.RegionEndpoint.SAEast1
			};

			return new AmazonDynamoDBClient(_awsAccessKey, _awsSecretKey, config);
		}

		public static Table GetTable(string tableName)
		{

			Table table = Table.LoadTable(GetClient(), tableName);

			if (table == null)
				throw new Exception("Tabela não encontrada no DynamoDB");

			return table;
		}
	}
}
