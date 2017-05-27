using System;
using System.ComponentModel.DataAnnotations;

namespace DACManager.Models
{
	public class Payment
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		[Display(Name = "Payment Date")]
		public DateTime PaymentDate { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		//public int StaffId { get; set; }
		//public Staff Staff { get; set; }

		//public int RentalId { get; set; }
		public Rental Rental { get; set; }
	}
}
