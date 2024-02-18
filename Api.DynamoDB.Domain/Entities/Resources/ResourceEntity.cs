namespace Api.DynamoDB.Domain.Entities.Resources
{
	public class ResourceEntity : BaseEntity
	{
		public string Owner { get; set; }
		public string Id { get; set; }
		public List<AttributeEntity> AttributesList { get; set; }
		public bool? UseCreatingLog { get; set; }
		public bool? UseUpdatingLog { get; set; }
		public bool? UseSoftDelete { get; set; }
		public bool? Active { get; set; }
	}
}
