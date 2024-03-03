namespace Api.DynamoDB.Domain.Enums
{
	public enum FieldFillingType
	{
		Required = 1,
		Optional = 2,
		ReadOnly = 3
	}

	public static class FieldFillingTypeExtensions
	{
		public static IEnumerable<string> GetValidValues()
		{
			return Enum.GetNames(typeof(FieldFillingType)).ToArray().ToList();
		}
	}
}
