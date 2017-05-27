using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACManager.Models
{
	public class Rental
	{
		public int Id { get; set; }

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		[Display(Name = "Rental Date")]
		public DateTime RentalDate { get; set; }

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		[Display(Name = "Return Date")]
		public DateTime ReturnDate { get; set; }

		public int InventoryId { get; set; }
		public Inventory Inventory { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		//public int StaffId { get; set; }
		public Staff Staff { get; set; }

		public ICollection<Payment> Payments { get; set; }
	}
}
