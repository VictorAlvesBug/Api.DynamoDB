using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Domain.Entities;
using Api.DynamoDB.Helpers.Extensions;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;
using Api.DynamoDB.Infrastructure.Utils;
using Newtonsoft.Json.Linq;

namespace Api.DynamoDB.Infrastructure.Repositories
{
	public abstract class GenericRepository<TEntity> : IRepository<TEntity>
		where TEntity : BaseEntity
	{
		public readonly Table _table;

		public GenericRepository(string tableName)
		{
			_table = DynamoUtils.GetTable(tableName);
		}

		private static Dictionary<string, string> GetKeysExpressionAttributeNames(string PK, string SK) => new()
		{
			{ $"#{nameof(PK)}", nameof(PK) },
			{ $"#{nameof(SK)}", nameof(SK) }
		};

		private static Dictionary<string, DynamoDBEntry> GetKeysExpressionAttributeValues(string PK, string SK) => new()
		{
			{ $":{nameof(PK)}", PK },
			{ $":{nameof(SK)}", SK }
		};

		private static PutItemOperationConfig GetDefaultPutItemOperationConfig(TEntity entity) =>
			GetDefaultPutItemOperationConfig(entity.PK, entity.SK);

		private static PutItemOperationConfig GetDefaultPutItemOperationConfig(string PK, string SK) =>
			new()
			{
				ReturnValues = ReturnValues.AllNewAttributes,
				ConditionalExpression = new Amazon.DynamoDBv2.DocumentModel.Expression
				{
					ExpressionAttributeNames = GetKeysExpressionAttributeNames(PK, SK),
					ExpressionAttributeValues = GetKeysExpressionAttributeValues(PK, SK),
					ExpressionStatement =
						$@"#{nameof(PK)} <> :{nameof(PK)} OR #{nameof(SK)} <> :{nameof(SK)}"
				}
			};

		private static UpdateItemOperationConfig GetDefaultUpdateItemOperationConfig(TEntity entity) =>
			GetDefaultUpdateItemOperationConfig(entity.PK, entity.SK);

		private static UpdateItemOperationConfig GetDefaultUpdateItemOperationConfig(string PK, string SK) =>
			new()
			{
				ReturnValues = ReturnValues.AllNewAttributes,
				ConditionalExpression = new Amazon.DynamoDBv2.DocumentModel.Expression
				{
					ExpressionAttributeNames = GetKeysExpressionAttributeNames(PK, SK),
					ExpressionAttributeValues = GetKeysExpressionAttributeValues(PK, SK),
					ExpressionStatement =
						$@"#{nameof(PK)} = :{nameof(PK)} AND #{nameof(SK)} = :{nameof(SK)}"
				}
			};

		private static DeleteItemOperationConfig GetDefaultDeleteItemOperationConfig(TEntity entity) =>
			GetDefaultDeleteItemOperationConfig(entity.PK, entity.SK);

		private static DeleteItemOperationConfig GetDefaultDeleteItemOperationConfig(string PK, string SK) =>
			new()
			{
				ReturnValues = ReturnValues.None,
				ConditionalExpression = new Amazon.DynamoDBv2.DocumentModel.Expression
				{
					ExpressionAttributeNames = GetKeysExpressionAttributeNames(PK, SK),
					ExpressionAttributeValues = GetKeysExpressionAttributeValues(PK, SK),
					ExpressionStatement =
						$@"#{nameof(PK)} = :{nameof(PK)} AND #{nameof(SK)} = :{nameof(SK)}"
				}
			};

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
			List<string> attributesToGet = null)
		{
			ScanFilter filter = new ScanFilter();

			Search search = _table.Scan(filter);
			List<Document> results = await search.GetNextSetAsync();

			return results.ConvertTo<TEntity>();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAsync(
			string PK,
			List<string> attributesToGet = null)
		{
			QueryFilter filter = new QueryFilter();
			filter.AddCondition(nameof(PK), QueryOperator.Equal, PK);

			QueryOperationConfig queryConfig =
				attributesToGet == null
				? new QueryOperationConfig
				{
					Filter = filter
				}
				: new QueryOperationConfig
				{
					Filter = filter/*,
					AttributesToGet = attributesToGet*/
				};

			Search search = _table.Query(queryConfig);
			List<Document> results = await search.GetNextSetAsync();

			return results.ConvertTo<TEntity>();
		}

		public virtual async Task<TEntity> GetSingleAsync(
			string PK, 
			string SK, 
			List<string> attributesToGet = null)
		{
			QueryFilter filter = new QueryFilter();
			filter.AddCondition(nameof(PK), QueryOperator.Equal, PK);
			filter.AddCondition(nameof(SK), QueryOperator.Equal, SK);

			QueryOperationConfig queryConfig =
				attributesToGet == null
				? new QueryOperationConfig
				{
					Filter = filter
				}
				: new QueryOperationConfig
				{
					Filter = filter,
					AttributesToGet = attributesToGet
				};

			Search search = _table.Query(queryConfig);
			List<Document> results = await search.GetNextSetAsync();

			return results.ConvertTo<TEntity>().FirstOrDefault();
		}

		public virtual async Task CreateItemAsync(TEntity entity)
		{
			await _table.PutItemAsync(
				entity.ToDocument(),
				GetDefaultPutItemOperationConfig(entity));
		}

		public virtual async Task UpdateWholeItemAsync(TEntity entity)
		{
			await _table.UpdateItemAsync(
				entity.ToDocument(),
				GetDefaultUpdateItemOperationConfig(entity));
		}

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

		public virtual async Task UpdatePartialItemAsync(TEntity entity)
		{
			/*var updateRequest = new UpdateItemRequest
			{
				TableName = _table.TableName,
				Key = new Dictionary<string, AttributeValue>
				{
					{ nameof(entity.PK), new AttributeValue { S = entity.PK } },
					{ nameof(entity.SK), new AttributeValue { S = entity.SK } }
				},
				UpdateExpression = "SET",
				ExpressionAttributeValues = new Dictionary<string, AttributeValue>(),
				ReturnValues = ReturnValue.ALL_NEW
			};

			var entityJObject = JObject.Parse(entity.ToJson());

			foreach (var property in entityJObject)
			{
				if (property.Key.NotIn(nameof(entity.PK), nameof(entity.SK), "$type") && property.Value != null)
				{
					updateRequest.UpdateExpression += $" #{property.Key} = :{property.Key},";
					updateRequest.ExpressionAttributeValues[$":{property.Key}"] = ConvertToAttributeValue(property.Value);
					updateRequest.ExpressionAttributeNames[$"#{property.Key}"] = property.Key;
				}
			}

			updateRequest.UpdateExpression = updateRequest.UpdateExpression.TrimEnd(',');

			await DynamoUtils.GetClient().UpdateItemAsync(updateRequest);*/

			await _table.UpdateItemAsync(
				entity.ToDocument(),
				GetDefaultUpdateItemOperationConfig(entity));
		}

		public virtual async Task DeleteAsync(string PK, string SK)
		{
			await _table.DeleteItemAsync(PK, SK, GetDefaultDeleteItemOperationConfig(PK, SK));
		}
	}
}
