using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DACManager.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		public bool IsAdmin { get; set; }

		public Permission Permission { get; set; }
	}
}
