using Api.DynamoDB.Domain.Enums;

namespace Api.DynamoDB.Domain.Entities.Resources
{
	public class AttributeEntity
	{
        public string Name { get; set; }
        public AttributeType Type { get; set; }
        public AttributeType ListItemType { get; set; }
        //public object MinimumValue { get; set; }
        //public object MaximumValue { get; set; }
        public FieldFillingType FillingTypeOnCreate { get; set; }
        public FieldFillingType FillingTypeOnUpdate { get; set; }
    }
}
