using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Services.Interfaces;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Application.Services
{
	public class TableService : ITableService
	{
		private readonly ITableRepository _repository;

        public TableService(ITableRepository repository)
        {
            _repository = repository;
        }

		public async Task<IEnumerable<string>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<TableDescription> DescribeAsync(string tableName)
		{
			return await _repository.DescribeAsync(tableName);
		}

		public async Task<TableDescription> CreateAsync(string tableName)
		{
			return await _repository.CreateAsync(tableName);
		}
	}
}