using Api.DynamoDB.Domain.Entities.Resources;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Infrastructure.Repositories
{
	public class ResourceRepository : GenericRepository<ResourceEntity>, IResourceRepository
	{
		private readonly static string _tableName = "nodb-resource";
		public ResourceRepository() : base(_tableName)
		{
		}

		public async Task DisableAsync(string pk, string sk)
		{
			var resource = new ResourceEntity
			{
				PK = pk,
				SK = sk,
				Active = false
			};

			await base.UpdatePartialItemAsync(resource);
		}
	}
}
