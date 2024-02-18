using Api.DynamoDB.Domain.Entities.Resources;

namespace Api.DynamoDB.Application.Models.Resources
{
	public class ResourceModel
	{
		public string Owner { get; set; }
		public string Id { get; set; }
		public List<AttributeModel> AttributesList { get; set; }
		public bool UseCreatingLog { get; set; }
		public bool UseUpdatingLog { get; set; }
		public bool UseSoftDelete { get; set; }
		public bool Active { get; set; }

	}
}
