using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models.Indexes;

namespace Api.DynamoDB.Application.Services.Interfaces
{
	public interface IIndexService
	{
		Task<TableIndexesModel> GetAllAsync(string tableName);
		Task<TableDescription> DescribeAsync(IndexToDescribeModel indexToDescribeModel);
		Task<TableDescription> CreateAsync(IndexToCreateModel indexToCreateModel);
		Task<TableDescription> DeleteAsync(IndexToDeleteModel indexToDeleteModel);
	}
}
