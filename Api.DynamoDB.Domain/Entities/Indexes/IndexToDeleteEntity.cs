namespace Api.DynamoDB.Domain.Entities.Indexes
{
	public class IndexToDeleteEntity
	{
		public string TableName { get; set; }
		public string IndexName { get; set; }
	}
}
