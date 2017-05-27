using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACManager.Models
{
	public class Actor
	{
		public int Id { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		[Display(Name = "Birth Date")]
		public DateTime BirthDate { get; set; }
		public ICollection<ActorMovie> ActorMovies { get; set; }
	}
}
