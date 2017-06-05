using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DACManager.Models
{
	[Flags]
	public enum TablePermission
	{
		[Display(Name = "-")]
		None = 0,
		[Display(Name = "s")]
		s = 1 << 0,
		[Display(Name = "i")]
		i = 1 << 1,
		[Display(Name = "d")]
		d = 1 << 2,
		[Display(Name = "u")]
		u = 1 << 3,
		[Display(Name = "S")]
		DelegateSelect = 1 << 4,
		[Display(Name = "I")]
		DelegateInsert = 1 << 5,
		[Display(Name = "D")]
		DelegateDelete = 1 << 6,
		[Display(Name = "U")]
		DelegateUpdate = 1 << 7,
		[Display(Name = "S")]
		S = s| DelegateSelect,
		[Display(Name = "I")]
		I = i | DelegateInsert,
		[Display(Name = "D")]
		D = d | DelegateDelete,
		[Display(Name = "U")]
		U = u | DelegateUpdate,
		//[Display(Name = "SIDU")]
		//Full = S | I | D | U,

	}

	public class Permission
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public string ParentId { get; set; }


		public TablePermission Actors { get; set; }
		public TablePermission Movies { get; set; }
		public TablePermission Categories { get; set; }
		public TablePermission Customers { get; set; }
		public TablePermission Inventories { get; set; }
		public TablePermission Languages { get; set; }
		public TablePermission Payments { get; set; }
		public TablePermission Permissions { get; set; }
		public TablePermission Rentals { get; set; }
		public TablePermission Staff { get; set; }
		public TablePermission Stores { get; set; }

		[Display(Name = "Can Take Over")]
		public bool CanTakeOver { get; set; }

		[Display(Name = "Can Create Users")]
		public bool CanCreateUsers { get; set; }

		[Display(Name = "Can View Permissions")]
		public bool CanViewPermissions { get; set; }

		[Display(Name = "Last Update")]
		public DateTime LastUpdate { get; set; }
	}
}
