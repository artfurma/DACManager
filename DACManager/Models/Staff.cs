using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACManager.Models
{
	public class Staff
	{
		public int Id { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }

		public int StoreId { get; set; }
		public Store Store { get; set; }

		public Payment Payment { get; set; }

		public ICollection<Rental> Rentals { get; set; }
	}
}
