using System.Collections.Generic;

namespace DACManager.Models
{
	public class Language
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
