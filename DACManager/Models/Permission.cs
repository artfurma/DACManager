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
		Select = 1 << 0,
		[Display(Name = "i")]
		Insert = 1 << 1,
		[Display(Name = "d")]
		Delete = 1 << 2,
		[Display(Name = "u")]
		Update = 1 << 3,
		[Display(Name="S")]
		DelegateSelect = 1 << 4,
		[Display(Name = "I")]
		DelegateInsert = 1 << 5,
		[Display(Name = "D")]
		DelegateDelete = 1 << 6,
		[Display(Name = "U")]
		DelegateUpdate = 1 << 7,
		//[Display(Name = "SI")]
		//SelectInsert = Select | Insert,
		//[Display(Name = "SD")]
		//SelectDelete = Select | Delete,
		//[Display(Name = "SU")]
		//SelectUpdate = Select | Update,
		//[Display(Name = "DU")]
		//DeleteUpdate = Delete | Update,
		//[Display(Name = "SID")]
		//InsertDelete = Select | Insert | Delete,
		//[Display(Name = "SIU")]
		//InsertUpdate = Select | Insert | Update,
		//[Display(Name = "SDU")]
		//SelectDeleteUpdate = Select | Delete | Update,
		//[Display(Name = "SIDU")]
		//InsertDeleteUpdate = Select | Insert | Delete | Update,
	}

	public class Permission
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

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

		public bool Receive { get; set; }

		[Display(Name = "Last Update")]
		public DateTime LastUpdate { get; set; }
	}
}
