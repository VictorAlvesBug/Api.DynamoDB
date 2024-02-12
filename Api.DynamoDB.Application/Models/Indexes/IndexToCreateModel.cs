namespace Api.DynamoDB.Application.Models.Indexes
{
	public class IndexToCreateModel
	{
        public string TableName { get; set; }
        public string IndexName { get; set; }
        public string PK { get; set; }
        public string SK { get; set; }
        public List<string> NonKeyAttributes { get; set; }
    }
}
