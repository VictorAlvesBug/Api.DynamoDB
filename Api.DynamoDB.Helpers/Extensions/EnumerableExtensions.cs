namespace Api.DynamoDB.Helpers.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool SafeAny<T>(this IEnumerable<T> list) => list != null && list.Any();

	}
}
