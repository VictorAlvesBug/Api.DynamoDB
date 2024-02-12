using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Domain.Entities;
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

		public async Task<TableDescription> CreateAsync(string tableName)
		{
			var client = DynamoUtils.GetClient();

			var request = new CreateTableRequest
			{
				TableName = tableName,
				KeySchema = new List<KeySchemaElement>
				{
					new KeySchemaElement
					{
						AttributeName = "PK",
						KeyType = "HASH"
					},
					new KeySchemaElement
					{
						AttributeName = "SK",
						KeyType = "RANGE"
					}
				},
				AttributeDefinitions = new List<AttributeDefinition>
				{
					new AttributeDefinition
					{
						AttributeName = "PK",
						AttributeType = "S"
					},
					new AttributeDefinition
					{
						AttributeName = "SK",
						AttributeType = "S"
					}
				},
				ProvisionedThroughput = new ProvisionedThroughput
				{
					ReadCapacityUnits = 5,
					WriteCapacityUnits = 5
				}
			};

			var response = await client.CreateTableAsync(request);

			return response.TableDescription;
		}
	}
}