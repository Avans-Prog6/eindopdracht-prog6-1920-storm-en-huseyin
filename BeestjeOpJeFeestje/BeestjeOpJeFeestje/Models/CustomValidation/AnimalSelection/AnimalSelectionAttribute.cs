using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models.Repositories;

namespace BeestjeOpJeFeestje.Models.CustomValidation.AnimalSelection
{
	public class AnimalSelectionAttribute : ValidationAttribute
	{
		public string GetErrorMessage() => "Gekozen dieren zijn onjuiste combinatie";

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
