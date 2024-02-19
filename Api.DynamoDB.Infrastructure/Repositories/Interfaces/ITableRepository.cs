using Amazon.DynamoDBv2.Model;

namespace Api.DynamoDB.Infrastructure.Repositories.Interfaces
{
	public interface ITableRepository
	{
		Task<IEnumerable<string>> GetAllAsync();
		Task<TableDescription> DescribeAsync(string tableName);
		Task<TableDescription> CreateAsync(
			string tableName,
			string pkName = "PK",
			string skName = "SK");
	}
}
