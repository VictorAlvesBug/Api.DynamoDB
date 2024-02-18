using Api.DynamoDB.Domain.Entities;

namespace Api.DynamoDB.Infrastructure.Repositories.Interfaces
{
	public interface IRepository<TEntity> where TEntity : BaseEntity
	{
		Task<IEnumerable<TEntity>> GetAllAsync(List<string> attributesToGet = null);
		Task<IEnumerable<TEntity>> GetAsync(string PK, List<string> attributesToGet = null);
		Task<TEntity> GetSingleAsync(string PK, string SK, List<string> attributesToGet = null);
		Task CreateItemAsync(TEntity entity);
		Task UpdateWholeItemAsync(TEntity entity);
		Task UpdatePartialItemAsync(TEntity entity);
		Task DeleteAsync(string PK, string SK);
	}
}
