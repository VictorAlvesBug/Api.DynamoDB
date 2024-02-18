using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using System.Globalization;

namespace Api.DynamoDB.Helpers.Extensions
{
	public static class DocumentExtensions
	{
		private readonly static JsonSerializerSettings DefaultSettings = new()
		{
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
			Culture = CultureInfo.CurrentCulture,
			DateFormatString = "yyyy-MM-dd",
			DateParseHandling = DateParseHandling.None,
			NullValueHandling = NullValueHandling.Ignore,
		};

		public static Document ToDocument<T>(this T obj) =>
			Document.FromJson(obj.ToJson());

		public static T ConvertTo<T>(this Document document)
		{
			if (document == null)
				return default;
			return JsonConvert.DeserializeObject<T>(document.ToJson(), DefaultSettings);
		}

		public static List<T> ConvertTo<T>(this List<Document> list) =>
			list.ConvertAll(i => ConvertTo<T>(i));

		public static IEnumerable<T> ConvertTo<T>(this IEnumerable<Document> list) =>
			list.Select(i => ConvertTo<T>(i));

		public static T ConvertTo<T>(this object obj)
		{
			if (obj == null)
				return default;
			return JsonConvert.DeserializeObject<T>(obj.ToJson(), DefaultSettings);
		}

		public static List<T> ConvertTo<T>(this List<object> list) =>
			list.ConvertAll(i => ConvertTo<T>(i));

		public static IEnumerable<T> ConvertTo<T>(this IEnumerable<object> list) =>
			list.Select(i => ConvertTo<T>(i));
	}
}
