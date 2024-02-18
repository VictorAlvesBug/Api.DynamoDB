using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models.Indexes;
using Api.DynamoDB.Application.Services.Interfaces;
using Api.DynamoDB.Domain.Entities.Indexes;
using Api.DynamoDB.Helpers.Extensions;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Application.Models.Indexes
{
	public class IndexToDeleteModel
	{
		public string TableName { get; set; }
		public string IndexName { get; set; }
	}
}
