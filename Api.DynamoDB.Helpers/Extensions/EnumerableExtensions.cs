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

		public static string AsString(this IEnumerable<string> list)
		{
			return list.JoinThis(lastSeparator: " e ");
		}

		public static string JoinThis(
			this IEnumerable<string> list,
			string separator = ", ",
			string lastSeparator = ", ")
		{
			int length = list.Count();

			if (length <= 1 || separator == lastSeparator)
				return string.Join(separator, list);

			string withoutLastOne = string.Join(separator, list.Take(length - 1));
			string lastOne = list.ElementAt(length - 1);

			return $"{withoutLastOne}{lastSeparator}{lastOne}";

		}
	}
}
