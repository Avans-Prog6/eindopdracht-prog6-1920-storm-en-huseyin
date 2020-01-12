using System;
using System.Collections.Generic;
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
		public string Address { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }

		public ClientInfo()
		{

		}


	}
}
