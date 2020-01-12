using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace BeestjeOpJeFeestje.Models.CustomValidation
{
	public class DateIsNotBookedAttributeProvider : AttributeAdapterBase<DateIsNotInThePastAttribute>
	{
		public DateIsNotBookedAttributeProvider(DateIsNotInThePastAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
		{
		}

		public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
			Attribute.GetErrorMessage();

		public override void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-date", GetErrorMessage(context));
		}
	}
}
