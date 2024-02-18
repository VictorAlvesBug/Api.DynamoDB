using Api.DynamoDB.Helpers.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Runtime.InteropServices.ObjectiveC;

namespace Api.DynamoDB.Helpers.Extensions
{
	public static class ObjectExtensions
	{
		private readonly static JsonSerializerSettings DefaultSettings = new()
		{
			Formatting = Formatting.None,
			ContractResolver = new JsonIgnorePropertyContractResolver(),
			Culture = CultureInfo.CurrentCulture,
			TypeNameHandling = TypeNameHandling.All,
			NullValueHandling = NullValueHandling.Ignore,
		};

		public static string ToJson(this object obj) =>
			JsonConvert.SerializeObject(obj, DefaultSettings);

		public static ObjectType MergeWith<ObjectType>(this ObjectType currentObject, ObjectType otherObject)
		{
			var current = JObject.Parse(currentObject.ToJson());
			var other = JObject.Parse(otherObject.ToJson());

			foreach (var property in other)
			{
				if (property.Value != null)
				{
					current[property.Key] = property.Value;
				}
			}

			return JsonConvert.DeserializeObject<ObjectType>(current.ToString(), DefaultSettings);
		}

		public static List<string> GetListOfAttributeNames<ObjectType>() =>
			typeof(ObjectType).GetProperties().Select(p => p.Name).ToList();

	}
}
