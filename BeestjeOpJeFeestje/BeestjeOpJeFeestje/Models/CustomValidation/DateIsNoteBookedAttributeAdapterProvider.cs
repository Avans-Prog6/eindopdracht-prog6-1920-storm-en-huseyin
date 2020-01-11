using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace BeestjeOpJeFeestje.Models.CustomValidation
{
	public class DateIsNoteBookedAttributeAdapterProvider : IValidationAttributeAdapterProvider
	{
		private readonly IValidationAttributeAdapterProvider baseProvider =
			new ValidationAttributeAdapterProvider();

		public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
			IStringLocalizer stringLocalizer)
		{
			if (attribute is DateIsNotBookedAttribute dateAttribute)
			{
				return new DateIsNotBookedAttributeProvider(dateAttribute, stringLocalizer);
			}

			return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
		}
	}
}
