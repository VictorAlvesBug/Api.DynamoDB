using Api.DynamoDB.Domain.Database;
using Api.DynamoDB.Domain.Entities.Resources;
using Api.DynamoDB.Helpers.Extensions;

namespace Api.DynamoDB.Application.Models.Resources
{
	public class ResourceToCreateModel
	{
		public string Id { get; set; }
		public bool UseCreatingLog { get; set; }
		public bool UseUpdatingLog { get; set; }
		public bool UseSoftDelete { get; set; }

		public ResourceEntity ToEntity(string owner)
		{
			var entity = this.ConvertTo<ResourceEntity>();
			entity.PK = Database.GetResourceSchemaPK(owner);
			entity.SK = Database.GetResourceSchemaSK(entity.Id);
			entity.AttributesList = new List<AttributeEntity>();
			entity.Owner = owner;
			entity.Active = true;
			return entity;
		}

		public void Validate()
		{
			if (string.IsNullOrEmpty(Id)) throw new ArgumentNullException($"O argumento '{nameof(Id)}' não pode ser nulo");
		}
	}
}
