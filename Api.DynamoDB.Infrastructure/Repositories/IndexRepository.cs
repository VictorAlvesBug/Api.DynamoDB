using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Domain.Entities;
using Api.DynamoDB.Domain.Entities.Indexes;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;
using Api.DynamoDB.Infrastructure.Utils;

namespace Api.DynamoDB.Infrastructure.Repositories
{
	public class IndexRepository : IIndexRepository
	{
		public async Task<TableDescription> DescribeAsync(IndexToDescribeEntity indexToDescribeEntity)
		{
			throw new NotImplementedException();
		}

		public async Task<TableDescription> CreateAsync(IndexToCreateEntity indexToCreateEntity)
		{
			var client = DynamoUtils.GetClient();

			var request = new UpdateTableRequest
			{
				TableName = indexToCreateEntity.TableName,
				GlobalSecondaryIndexUpdates = new List<GlobalSecondaryIndexUpdate>
				{
					new GlobalSecondaryIndexUpdate
					{
						Create = new CreateGlobalSecondaryIndexAction {
							IndexName = indexToCreateEntity.IndexName,
							KeySchema = new List<KeySchemaElement>
							{
								new KeySchemaElement
								{
									AttributeName = indexToCreateEntity.PK,
									KeyType = "HASH"
								},
								new KeySchemaElement
								{
									AttributeName = indexToCreateEntity.SK,
									KeyType = "RANGE"
								}
							},
							Projection = new Projection
							{
								ProjectionType =
									indexToCreateEntity.NonKeyAttributes?.Any() == true
									? ProjectionType.INCLUDE
									: ProjectionType.KEYS_ONLY,
								NonKeyAttributes = indexToCreateEntity.NonKeyAttributes
							},
							ProvisionedThroughput = new ProvisionedThroughput
							{
								ReadCapacityUnits = 5,
								WriteCapacityUnits = 5
							}
						}
					}
				}
			};

			var response = await client.UpdateTableAsync(request);

			return response?.TableDescription;
		}

		public async Task<TableDescription> DeleteAsync(IndexToDeleteEntity indexToDeleteEntity)
		{
			var client = DynamoUtils.GetClient();

			var request = new UpdateTableRequest
			{
				TableName = indexToDeleteEntity.TableName,
				GlobalSecondaryIndexUpdates = new List<GlobalSecondaryIndexUpdate>
				{
					new GlobalSecondaryIndexUpdate
					{
						Delete = new DeleteGlobalSecondaryIndexAction 
						{
							IndexName = indexToDeleteEntity.IndexName
						}
					}
				}
			};

			var response = await client.UpdateTableAsync(request);

			return response?.TableDescription;
		}
	}
}