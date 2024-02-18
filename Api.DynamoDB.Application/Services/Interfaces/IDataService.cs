namespace Api.DynamoDB.Application.Services.Interfaces
{
	public interface IDataService
	{
		Task<IEnumerable<object>> GetAllAsync(string tableName);
		Task<object> GetAsync(string tableName, string id);
		Task<object> CreateAsync(string tableName, object payload);
		Task<object> UpdateAsync(string tableName, string id, object payload);
		Task<bool> DeleteAsync(string tableName, string id);
	}
}
