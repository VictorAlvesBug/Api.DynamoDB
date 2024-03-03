using Api.DynamoDB.Application.Models.Attributes;
using Api.DynamoDB.Domain.Database;
using Api.DynamoDB.Domain.Entities.Resources;
using Api.DynamoDB.Helpers.Extensions;

namespace Api.DynamoDB.Application.Models.Resources
{
	public class ResourceToCreateModel
	{
		public string Id { get; set; }
		public List<AttributeToCreateModel> AttributesList { get; set; }
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
			if (string.IsNullOrEmpty(Id)) throw new ArgumentNullException($"Informe o '{nameof(Id)}' do recurso");
			if (!AttributesList.SafeAny()) throw new ArgumentException($"Adicione ao menos um atributo ao '{nameof(AttributesList)}'");

			for (int i = 0; i < AttributesList.Count; i++)
			{
				AttributesList[i].Validate(i);
			}

			if (!AttributesList.SafeAny()) throw new ArgumentException($"Adicione ao menos um atributo ao '{nameof(AttributesList)}'");
		}
	}
}
