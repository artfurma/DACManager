using System.Collections.Generic;

namespace DACManager.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<MovieCategory> MovieCategories { get; set; }
	}
}
