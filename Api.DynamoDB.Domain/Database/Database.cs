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

		public static string BuildPath(params string[] parts)
		{
			if (parts.Length == 0)
				return string.Empty;

			parts = parts.Select(part => part.Trim('/')).ToArray();

			return string.Join("/", parts);
		}
	}
}
