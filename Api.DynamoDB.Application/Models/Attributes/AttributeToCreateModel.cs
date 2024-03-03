using Api.DynamoDB.Domain.Enums;
using Api.DynamoDB.Helpers.Extensions;

namespace Api.DynamoDB.Application.Models.Attributes
{
	public class AttributeToCreateModel
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string? ListItemType { get; set; }
		//public object MinimumValue { get; set; }
		//public object MaximumValue { get; set; }
		public string FillingTypeOnCreate { get; set; }
		public string FillingTypeOnUpdate { get; set; }

		public void Validate(int? index = null)
		{
			var order = index == null ? " " : $" {index + 1}° ";

			if (string.IsNullOrEmpty(Name))
				throw new ArgumentException($"Informe o '{nameof(Name)}' do{order}atributo");

			var strValidValuesForAttributes = AttributeTypeExtensions.GetValidValuesForAttributes().AsString();

			if (!Enum.TryParse(Type, out AttributeType attributeType) 
				|| !attributeType.IsValidForAttributes())
				throw new ArgumentException($"O campo '{nameof(Type)}' aceita apenas os valores {strValidValuesForAttributes}");

			if (attributeType == AttributeType.List)
			{
				var strValidValuesForListItems = AttributeTypeExtensions.GetValidValuesForListItems().AsString();

				if (!Enum.TryParse(ListItemType, out AttributeType listItemAttributeType)
					|| !listItemAttributeType.IsValidForListItems())
					throw new ArgumentException($"O campo '{nameof(ListItemType)}' aceita apenas os valores {strValidValuesForListItems}");
			}

			var strValidValuesForFillingType = FieldFillingTypeExtensions.GetValidValues().AsString();

			if (!Enum.TryParse(FillingTypeOnCreate, out FieldFillingType _))
				throw new ArgumentException($"O campo '{nameof(FillingTypeOnCreate)}' aceita apenas os valores {strValidValuesForFillingType}");

			if (!Enum.TryParse(FillingTypeOnUpdate, out FieldFillingType _))
				throw new ArgumentException($"O campo '{nameof(FillingTypeOnUpdate)}' aceita apenas os valores {strValidValuesForFillingType}");
		}
	}
}
