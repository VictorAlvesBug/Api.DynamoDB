using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;
using Api.DynamoDB.Infrastructure.Utils;

namespace Api.DynamoDB.Infrastructure.Repositories
{
	public class TableRepository : ITableRepository
	{
		public async Task<IEnumerable<string>> GetAllAsync()
		{
			var client = DynamoUtils.GetClient();

			var request = new ListTablesRequest
			{
				Limit = 50
			};

			var response = await client.ListTablesAsync(request);

			return response.TableNames;
		}

		public async Task<TableDescription> DescribeAsync(string tableName)
		{
			var client = DynamoUtils.GetClient();

			var request = new DescribeTableRequest
			{
				TableName = tableName
			};

			var response = await client.DescribeTableAsync(request);

			return response?.Table;
		}

		public async Task<TableDescription> CreateAsync(
			string tableName, 
			string pkName = "PK", 
			string skName = "SK")
		{
			var client = DynamoUtils.GetClient();

			var request = new CreateTableRequest
			{
				TableName = tableName,
				KeySchema = new List<KeySchemaElement>
				{
					new KeySchemaElement
					{
						AttributeName = pkName,
						KeyType = "HASH"
					}
				},
				AttributeDefinitions = new List<AttributeDefinition>
				{
					new AttributeDefinition
					{
						AttributeName = pkName,
						AttributeType = "S"
					}
				},
				ProvisionedThroughput = new ProvisionedThroughput
				{
					ReadCapacityUnits = 5,
					WriteCapacityUnits = 5
				}
			};

			if (!string.IsNullOrEmpty(skName))
			{
				request.KeySchema
					.Add(new KeySchemaElement
					{
						AttributeName = skName,
						KeyType = "RANGE"
					});

				request.AttributeDefinitions
					.Add(new AttributeDefinition
					{
						AttributeName = skName,
						AttributeType = "S"
					});
			}

			var response = await client.CreateTableAsync(request);

			return response.TableDescription;
		}
	}
}