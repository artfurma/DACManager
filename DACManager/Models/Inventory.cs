namespace DACManager.Models
{
	public class Inventory
	{
		public int Id { get; set; }
		public int StoreId { get; set; }

		public Movie Movie { get; set; }

		public Rental Rental { get; set; }
	}
}
