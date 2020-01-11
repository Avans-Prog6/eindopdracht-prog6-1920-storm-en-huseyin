using System;
using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Models.Repositories;

namespace BeestjeOpJeFeestje.Models.CustomValidation
{
	public class DateIsNotBookedAttribute : ValidationAttribute
	{
		public string GetErrorMessage() => "De gekozen datum is al geboekt.";

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			IRepository<Booking> context =
				(IRepository<Booking>) validationContext.GetService(typeof(IRepository<Booking>));

			if (context == null)
			{
				throw new ApplicationException("Context was empty");
			}

			return context.Exists((Booking) validationContext.ObjectInstance) ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
		}
	}
}
