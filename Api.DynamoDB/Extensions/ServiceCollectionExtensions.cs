using Api.DynamoDB.Application.Services;
using Api.DynamoDB.Application.Services.Interfaces;
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
			//builder.AddScoped<IDataService, DataService>();

			return builder;
		}
		public static IServiceCollection AddRepositories(this IServiceCollection builder)
		{
			builder.AddScoped<ITableRepository, TableRepository>();
			builder.AddScoped<IIndexRepository, IndexRepository>();
			//builder.AddScoped<IDataRepository, DataRepository>();

			return builder;
		}
	}
}
