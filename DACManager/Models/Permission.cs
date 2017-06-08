using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
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
		[Display(Name = "S")]
		DelegateSelect = 1 << 4,
		[Display(Name = "I")]
		DelegateInsert = 1 << 5,
		[Display(Name = "D")]
		DelegateDelete = 1 << 6,
		[Display(Name = "U")]
		DelegateUpdate = 1 << 7,
		[Display(Name = "S")]
		S = Select | DelegateSelect,
		[Display(Name = "I")]
		I = Insert | DelegateInsert,
		[Display(Name = "D")]
		D = Delete | DelegateDelete,
		[Display(Name = "U")]
		U = Update | DelegateUpdate,
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

		[Display(Name = "Last Update")]
		public DateTime LastUpdate { get; set; }

		public string GetStringFromPermission(TablePermission permission)
		{
			var flags = Enum.GetValues(typeof(TablePermission));
			var result = "----";
			var sb = new StringBuilder(result);

			foreach (TablePermission flag in flags)
			{
				if (!permission.HasFlag(flag)) continue;
				var flagName = flag.GetType().GetMember(flag.ToString())
					.First().GetCustomAttribute<DisplayAttribute>().GetName();

				switch (flagName)
				{
					case "s":
						sb[0] = 's';
						break;
					case "i":
						sb[1] = 'i';
						break;
					case "d":
						sb[2] = 'd';
						break;
					case "u":
						sb[3] = 'u';
						break;
					case "S":
						sb[0] = 'S';
						break;
					case "I":
						sb[1] = 'I';
						break;
					case "D":
						sb[2] = 'D';
						break;
					case "U":
						sb[3] = 'U';
						break;

				}
				result = sb.ToString();
			}

			return result;
		}
	}
}
