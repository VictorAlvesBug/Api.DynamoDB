namespace Api.DynamoDB.Helpers.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool SafeAny<T>(
			this IEnumerable<T> list, 
			Func<T, bool> predicate = null)
		{
			if (list == null) return false;
			
			if (predicate == null) return list.Any();
			
			return list.Any(predicate);
		}

		public static List<TResult> ConvertAll<TItem, TResult>(
			this IEnumerable<TItem> list,
			Func<TItem, TResult> predicate) => 
			list.Select(predicate).ToList();
	}
}
