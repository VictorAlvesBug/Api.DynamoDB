namespace Api.DynamoDB.Application.Models.Indexes
{
	public class IndexToDeleteModel
	{
		public string TableName { get; set; }
		public string IndexName { get; set; }
	}
}
