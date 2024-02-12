using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.DynamoDB.Helpers.Utils
{
	public class JsonIgnorePropertyContractResolver : DefaultContractResolver
	{
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);

            foreach (JsonProperty prop in list)
            {
				prop.PropertyName = prop.UnderlyingName;
            }

            return list;
		}
	}
}
