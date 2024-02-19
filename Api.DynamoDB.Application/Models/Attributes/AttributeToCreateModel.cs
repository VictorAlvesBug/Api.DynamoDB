using Api.DynamoDB.Domain.Enums;

namespace Api.DynamoDB.Application.Models.Attributes
{
	public class AttributeToCreateModel
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string? ListItemType { get; set; }
		public object MinimumValue { get; set; }
		public object MaximumValue { get; set; }
		public FieldFillingType FillingTypeOnCreate { get; set; }
		public FieldFillingType FillingTypeOnUpdate { get; set; }

		/*public void Validate()
		{
			Enum.TryParse(Type, out AttributeType attributeType);

			switch (attributeType)
			{
				case AttributeType.Bool
			}
		}*/
	}
}
