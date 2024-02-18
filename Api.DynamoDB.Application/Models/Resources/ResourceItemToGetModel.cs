namespace Api.DynamoDB.Application.Models.Resources
{
	public class ResourceItemToGetModel
	{
		public string Id { get; set; }
		public bool UseCreatingLog { get; set; }
		public bool UseUpdatingLog { get; set; }
		public bool UseSoftDelete { get; set; }
		public bool Active { get; set; }
	}
}
