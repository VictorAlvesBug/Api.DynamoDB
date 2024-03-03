namespace Api.DynamoDB.Domain.Enums
{
	public enum AttributeType
	{
		Bool = 1,
		Number = 2,
		Decimal = 3,
		String = 4,
		Date = 5,
		DateTime = 6,
		List = 7
	}

	public static class AttributeTypeExtensions
	{
		public static IEnumerable<string> GetValidValuesForAttributes()
		{
			return Enum.GetNames(typeof(AttributeType)).ToArray().ToList();
		}

		public static IEnumerable<string> GetValidValuesForListItems()
		{
			return Enum.GetNames(typeof(AttributeType)).ToList().Where(name => name != nameof(AttributeType.List));
		}

		public static bool IsValidForAttributes(this AttributeType attributeType)
		{
			return GetValidValuesForAttributes().Contains(attributeType.ToString());
		}

		public static bool IsValidForListItems(this AttributeType attributeType)
		{
			return GetValidValuesForListItems().Contains(attributeType.ToString());
		}
	}
}
