using Amazon.DynamoDBv2.DocumentModel;

namespace Api.DynamoDB.Infrastructure.Repositories.Interfaces
{
	public interface IDataRepository
	{
		Task<IEnumerable<Document>> GetAllAsync(string tableName);
		Task<Document> GetAsync(string tableName, string id);
		Task<Document> CreateAsync(string tableName, Document documentToCreate);
		Task<Document> UpdateAsync(string tableName, string id, Document documentToUpdate);
		Task<bool> DeleteAsync(string tableName, string id);
	}
}
