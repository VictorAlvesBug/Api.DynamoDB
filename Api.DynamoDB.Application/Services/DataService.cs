using Api.DynamoDB.Application.Services.Interfaces;
using Api.DynamoDB.Helpers.Extensions;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Application.Services
{
	public class DataService : IDataService
	{
		private readonly IDataRepository _repository;
        public DataService(
			IDataRepository repository)
        {
			_repository = repository;

		}

		public async Task<IEnumerable<object>> GetAllAsync(string tableName)
		{
			return await _repository.GetAllAsync(tableName);
		}

		public async Task<object> GetAsync(string tableName, string id)
		{
			return await _repository.GetAsync(tableName, id);
		}
        public async Task<object> CreateAsync(string tableName, object payload)
		{
			return await _repository.CreateAsync(tableName, payload.ToDocument());
		}

		public async Task<object> UpdateAsync(string tableName, string id, object payload)
		{
			return await _repository.UpdateAsync(tableName, id, payload.ToDocument());
		}

		public async Task<bool> DeleteAsync(string tableName, string id)
		{
			return await _repository.DeleteAsync(tableName, id);
		}
	}
}
