using System;
using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Models.Repositories;

namespace BeestjeOpJeFeestje.Models.CustomValidation
{
	public class DateIsNotInThePastAttribute : ValidationAttribute
	{
		public string GetErrorMessage() => "The chosen date may not be in the past";

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			IRepository<Booking> context =
				(IRepository<Booking>) validationContext.GetService(typeof(IRepository<Booking>));

			if (context == null)
			{
				throw new ApplicationException("Context was empty");
			}

			Booking booking = (Booking) validationContext.ObjectInstance;

			return (booking.Date < DateTime.Now) ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
		}
	}
}
