using Api.DynamoDB.Domain.Database;
using Api.DynamoDB.Domain.Entities.Resources;
using Api.DynamoDB.Helpers.Extensions;

namespace Api.DynamoDB.Application.Models.Resources
{
	public class ResourceToUpdateModel
	{
		public bool? UseCreatingLog { get; set; }
		public bool? UseUpdatingLog { get; set; }
		public bool? UseSoftDelete { get; set; }

		public ResourceEntity ToEntity(string owner, string id = null)
		{
			var entity = this.ConvertTo<ResourceEntity>();
			entity.Id ??= id;
			entity.Owner = owner;
			entity.PK = Database.GetResourceSchemaPK(entity.Owner);
			entity.SK = Database.GetResourceSchemaSK(entity.Id);
			entity.AttributesList = new List<AttributeEntity>();
			entity.Active = true;
			return entity;
		}
	}
}
