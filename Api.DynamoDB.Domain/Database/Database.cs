namespace Api.DynamoDB.Domain.Database
{
	public static partial class Database
	{
		public static string Hyphenize(params string[] parts)
		{
			if (parts.Length == 0)
				return string.Empty;

			return string.Join("-", parts);
		}
	}
}
