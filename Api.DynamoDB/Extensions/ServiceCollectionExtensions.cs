using Api.DynamoDB.Application.Services;
using Api.DynamoDB.Application.Services.Interfaces;
using Api.DynamoDB.Domain.Entities.Resources;
using Api.DynamoDB.Infrastructure.Repositories;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Web.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection builder)
		{
			builder.AddScoped<ITableService, TableService>();
			builder.AddScoped<IIndexService, IndexService>();
			builder.AddScoped<IDataService, DataService>();
			builder.AddScoped<IResourceService, ResourceService>();

			return builder;
		}
		public static IServiceCollection AddRepositories(this IServiceCollection builder)
		{
			builder.AddScoped<ITableRepository, TableRepository>();
			builder.AddScoped<IIndexRepository, IndexRepository>();
			builder.AddScoped<IDataRepository, DataRepository>();
			builder.AddScoped<IResourceRepository, ResourceRepository>();
			builder.AddScoped<IRepository<ResourceEntity>, ResourceRepository>();

			return builder;
		}
	}
}
