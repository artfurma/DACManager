using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DACManager.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Length { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }

		//public int LanguageId { get; set; }
		public Language Language { get; set; }

		public ICollection<Inventory> Inventories { get; set; }

		public ICollection<ActorMovie> ActorMovies { get; set; }

		public ICollection<MovieCategory> MovieCategories { get; set; }

	}
}
