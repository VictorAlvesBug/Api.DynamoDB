using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Domain.Entities.Indexes;

namespace Api.DynamoDB.Infrastructure.Repositories.Interfaces
{
	public interface IIndexRepository
	{
		Task<TableDescription> DescribeAsync(IndexToDescribeEntity indexToDescribeEntity);
		Task<TableDescription> CreateAsync(IndexToCreateEntity indexToCreateEntity);
		Task<TableDescription> DeleteAsync(IndexToDeleteEntity indexToDeleteEntity);
	}
}
