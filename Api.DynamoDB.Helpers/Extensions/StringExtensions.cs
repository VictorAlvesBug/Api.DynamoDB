namespace Api.DynamoDB.Helpers.Extensions
{
	public static class StringExtensions
	{
		public static bool In(this string text, params string[] items) =>
			items.SafeAny(item => item == text);
		public static bool NotIn(this string text, params string[] items) =>
			!text.In(items);
	}
}
