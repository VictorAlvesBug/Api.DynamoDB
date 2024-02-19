using Amazon.DynamoDBv2.Model;

namespace Api.DynamoDB.Application.Services.Interfaces
{
	public interface ITableService
	{
		Task<IEnumerable<string>> GetAllAsync();
		Task<TableDescription> DescribeAsync(string tableName);
		Task<TableDescription> CreateAsync(string tableName);
	}
}
