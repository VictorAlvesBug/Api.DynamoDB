using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models.Indexes;
using Api.DynamoDB.Application.Services.Interfaces;
using Api.DynamoDB.Domain.Entities.Indexes;
using Api.DynamoDB.Helpers.Extensions;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Application.Services
{
	public class IndexService : IIndexService
	{
		private readonly IIndexRepository _indexRepository;
		private readonly ITableRepository _tableRepository;

        public IndexService(
			IIndexRepository indexRepository,
			ITableRepository tableRepository)
        {
			_indexRepository = indexRepository;
			_tableRepository = tableRepository;

		}

        public async Task<TableIndexesModel> GetAllAsync(string tableName)
		{
			var tableDescription = await _tableRepository.DescribeAsync(tableName);
			return new TableIndexesModel
			{
				GlobalSecondaryIndexes = tableDescription.GlobalSecondaryIndexes,
				LocalSecondaryIndexes = tableDescription.LocalSecondaryIndexes
			};
		}

		public async Task<TableDescription> DescribeAsync(IndexToDescribeModel indexToDescribeModel)
		{
			/*var tableDescription = await _tableRepository.DescribeAsync(indexToDescribeModel.TableName);

			var a = tableDescription.GlobalSecondaryIndexes.

			return new TableIndexesModel
			{
				GlobalSecondaryIndexes = tableDescription.GlobalSecondaryIndexes,
				LocalSecondaryIndexes = tableDescription.LocalSecondaryIndexes
			};*/

			return null;
		}

		public async Task<TableDescription> CreateAsync(IndexToCreateModel indexToCreateModel)
		{
			return await _indexRepository.CreateAsync(indexToCreateModel.ConvertTo<IndexToCreateEntity>());
		}

		public async Task<TableDescription> DeleteAsync(IndexToDeleteModel indexToDeleteModel)
		{
			return await _indexRepository.DeleteAsync(indexToDeleteModel.ConvertTo<IndexToDeleteEntity>());
		}
	}
}
