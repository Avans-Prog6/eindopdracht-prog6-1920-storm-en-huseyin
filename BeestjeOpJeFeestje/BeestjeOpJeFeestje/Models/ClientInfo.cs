using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
	public class ClientInfo
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[Required, Display(Name = "Name")]
		public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
		public string MiddleName { get; set; }
		[Required, Display(Name = "Surname")]
		public string LastName { get; set; }
		[Required]
		public string Address { get; set; }
		[EmailAddress]
		public string Email { get; set; }

		[DisplayName("Phone Number")]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "The PhoneNumber field is not a valid phone number")]
		public string PhoneNumber { get; set; }

		public ClientInfo()
		{

		}


	}
}
