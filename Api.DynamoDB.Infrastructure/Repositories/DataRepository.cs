using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Helpers.Extensions;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;
using Api.DynamoDB.Infrastructure.Utils;
using Newtonsoft.Json.Linq;

namespace Api.DynamoDB.Infrastructure.Repositories
{
	public class DataRepository : IDataRepository
	{
		private static Dictionary<string, string> GetKeysExpressionAttributeNames(string id) => new()
		{
			{ $"#{nameof(id)}", nameof(id) }
		};

		private static Dictionary<string, DynamoDBEntry> GetKeysExpressionAttributeValues(string id) => new()
		{
			{ $":{nameof(id)}", id }
		};

		private static PutItemOperationConfig GetDefaultPutItemOperationConfig(string id) =>
			new()
			{
				ReturnValues = ReturnValues.AllNewAttributes,
				ConditionalExpression = new Expression
				{
					ExpressionAttributeNames = GetKeysExpressionAttributeNames(id),
					ExpressionAttributeValues = GetKeysExpressionAttributeValues(id),
					ExpressionStatement =
						$@"#{nameof(id)} <> :{nameof(id)}"
				}
			};

		private static DeleteItemOperationConfig GetDefaultDeleteItemOperationConfig(string id) =>
			new()
			{
				ReturnValues = ReturnValues.None,
				ConditionalExpression = new Expression
				{
					ExpressionAttributeNames = GetKeysExpressionAttributeNames(id),
					ExpressionAttributeValues = GetKeysExpressionAttributeValues(id),
					ExpressionStatement =
						$@"#{nameof(id)} = :{nameof(id)}"
				}
			};

		private static AttributeValue ConvertToAttributeValue(JToken token)
		{
			JTokenType a = token.Type;

			switch (token.Type)
			{
				case JTokenType.Boolean:
					return new AttributeValue { BOOL = (bool)token, IsBOOLSet = true };
				case JTokenType.Integer:
					return new AttributeValue { N = (string)token };
				case JTokenType.Float:
					return new AttributeValue { N = (string)token };
				case JTokenType.String:
					return new AttributeValue { S = (string)token };
				case JTokenType.Date:
					return new AttributeValue { S = ((DateTime)token).ToString("yyyy-MM-dd") };
				case JTokenType.Array:
					return new AttributeValue
					{
						L = token.ToList().ConvertAll(item => ConvertToAttributeValue(item)),
						IsLSet = true
					};
				case JTokenType.Object:
					return new AttributeValue
					{
						M = ((JObject)token).Properties()
								.ToDictionary(p => p.Name, p => ConvertToAttributeValue(p.Value)),
						IsMSet = true
					};
				case JTokenType.None:
				case JTokenType.Null:
				case JTokenType.Undefined:
					return new AttributeValue { NULL = true };
			}

			return new AttributeValue { S = (string)token };
		}

		public async Task<IEnumerable<Document>> GetAllAsync(string tableName)
		{
			ScanFilter filter = new();

			var table = DynamoUtils.GetTable(tableName);
			Search search = table.Scan(filter);
			return await search.GetNextSetAsync();
		}

		public async Task<Document> GetAsync(string tableName, string id)
		{
			QueryFilter filter = new QueryFilter();
			filter.AddCondition(nameof(id), QueryOperator.Equal, id);

			QueryOperationConfig queryConfig = new QueryOperationConfig
			{
				Filter = filter
			};

			var table = DynamoUtils.GetTable(tableName);
			Search search = table.Query(queryConfig);
			return (await search.GetNextSetAsync()).FirstOrDefault();
		}

		public async Task<Document> CreateAsync(string tableName, Document documentToCreate)
		{
			var table = DynamoUtils.GetTable(tableName);
			var document = await table.PutItemAsync(
				documentToCreate,
				GetDefaultPutItemOperationConfig(documentToCreate["id"]));

			return document;
		}

		public async Task<Document> UpdateAsync(string tableName, string id, Document documentToUpdate)
		{
			var table = DynamoUtils.GetTable(tableName);

			var updateRequest = new UpdateItemRequest
			{
				TableName = table.TableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{ nameof(id), new AttributeValue { S = id } }
				},
				UpdateExpression = "SET",
				ExpressionAttributeValues = new Dictionary<string, AttributeValue>(),
				ReturnValues = ReturnValue.ALL_NEW
			};

			var entityJObject = JObject.Parse(documentToUpdate.ToJson());

			foreach (var property in entityJObject)
			{
				if (property.Key.NotIn(nameof(id), "$type") && property.Value != null)
				{
					updateRequest.UpdateExpression += $" #{property.Key} = :{property.Key},";
					updateRequest.ExpressionAttributeValues[$":{property.Key}"] = ConvertToAttributeValue(property.Value);
					updateRequest.ExpressionAttributeNames[$"#{property.Key}"] = property.Key;
				}
			}

			updateRequest.UpdateExpression = updateRequest.UpdateExpression.TrimEnd(',');

			await DynamoUtils.GetClient().UpdateItemAsync(updateRequest);

			return null;
		}

		public async Task<bool> DeleteAsync(string tableName, string id)
		{
			var table = DynamoUtils.GetTable(tableName);
			await table.DeleteItemAsync(id, GetDefaultDeleteItemOperationConfig(id));
			return true;
		}
	}
}
