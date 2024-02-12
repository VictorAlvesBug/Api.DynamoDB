namespace Api.DynamoDB.Domain.Entities.Indexes
{
	public class IndexToCreateEntity
	{
        public string TableName { get; set; }
        public string IndexName { get; set; }
        public string PK { get; set; }
        public string SK { get; set; }
        public List<string> NonKeyAttributes { get; set; }
    }
}
