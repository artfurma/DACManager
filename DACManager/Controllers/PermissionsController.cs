using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACManager.Data;
using DACManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;

namespace DACManager.Controllers
{
	public class PermissionsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public PermissionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Permissions
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Permissions.Include(p => p.User);
			ViewData["Users"] = _context.Users.ToList();
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Permissions/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var permission = await _context.Permissions
				.Include(p => p.User)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (permission == null)
			{
				return NotFound();
			}

			return View(permission);
		}

		// GET: Permissions/Create
		public IActionResult Create()
		{
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
			return View();
		}

		// POST: Permissions/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,UserId,Actors,Movies,Categories,Customers,Inventories,Languages,Payments,Permissions,Rentals,Staff,Stores,CanTakeOver,CanCreateUsers,LastUpdate")] Permission permission)
		{
			if (ModelState.IsValid)
			{
				_context.Add(permission);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", permission.UserId);
			return View(permission);
		}

		// GET: Permissions/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var permission = await _context.Permissions.Include(p => p.User).SingleOrDefaultAsync(p => p.Id == id);
			if (permission == null)
			{
				return NotFound();
			}
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", permission.UserId);
			return View(permission);
		}

		// POST: Permissions/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ParentId,User,Actors,Movies,Categories,Customers,Inventories,Languages,Payments,Permissions,Rentals,Staff,Stores,CanTakeOver,CanCreateUsers,LastUpdate")] Permission permission)
		{
			if (id != permission.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
					loggedInUser.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == loggedInUser.Id);

					var userPermission = _context.Permissions.AsNoTracking().FirstOrDefault(p => p.UserId == permission.UserId);

					if (userPermission != null && userPermission.CanTakeOver)
					{
						TakeOverPermissions(loggedInUser.Permission, permission);
						RemoveAllPermissions(loggedInUser.Permission);

						_context.Update(permission);

						var children = GetAllChildren(loggedInUser.Permission);
						foreach (var child in children)
						{
							if (child.UserId == permission.UserId) continue;
							if (userPermission.CanTakeOver)
							{
								RemoveAllPermissions(child);
							}
							else
							{
								UpdatePermissions(child, permission);
							}
							_context.Update(child);
						}
					}
					else
					{
						var children = GetAllChildren(permission);
						foreach (var child in children)
						{
							if (child.UserId == permission.UserId) continue;
							if (userPermission != null && userPermission.CanTakeOver)
							{
								RemoveAllPermissions(child);
							}
							else
							{
								UpdatePermissions(child, permission);
							}
							_context.Update(child);
						}
					}

					permission.LastUpdate = DateTime.Now;
					_context.Update(permission);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PermissionExists(permission.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				return RedirectToAction("Index");
			}
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", permission.UserId);
			return View(permission);
		}

		public IEnumerable<Permission> GetAllChildren(Permission permission)
		{
			var children = new HashSet<Permission>();

			var child = _context.Permissions.FirstOrDefault(p => p.ParentId == permission.UserId);

			while (child != null)
			{
				children.Add(child);
				child = _context.Permissions.FirstOrDefault(p => p.ParentId == child.UserId);
			}

			return children;
		}

		public void RemoveAllPermissions(Permission permission)
		{
			permission.Actors = TablePermission.None;
			permission.Movies = TablePermission.None;
			permission.Categories = TablePermission.None;
			permission.Languages = TablePermission.None;
			permission.Customers = TablePermission.None;
			permission.Staff = TablePermission.None;
			permission.Stores = TablePermission.None;
			permission.CanCreateUsers = false;
			permission.CanTakeOver = false;
		}

		public void TakeOverPermissions(Permission victim, Permission target)
		{
			target.Actors = victim.Actors;
			target.Movies = victim.Movies;
			target.Categories = victim.Categories;
			target.Languages = victim.Languages;
			target.Customers = victim.Customers;
			target.Staff = victim.Staff;
			target.Stores = victim.Stores;
			target.CanCreateUsers = victim.CanCreateUsers;
			target.CanTakeOver = false;
			target.ParentId = victim.ParentId;

			if (!victim.User.IsAdmin) return;
			var targetUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == target.UserId);
			targetUser.IsAdmin = true;
			_context.Update(targetUser);

			victim.User.IsAdmin = false;
			_context.Update(victim.User);
		}
		public void UpdatePermissions(Permission child, Permission parent)
		{
			// SELECT
			if ((parent.Actors & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Actors &= ~TablePermission.S;
			}
			if ((parent.Movies & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Movies &= ~TablePermission.S;
			}
			if ((parent.Categories & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Categories &= ~TablePermission.S;
			}
			if ((parent.Languages & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Languages &= ~TablePermission.S;
			}
			if ((parent.Customers & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Customers &= ~TablePermission.S;
			}
			if ((parent.Staff & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Staff &= ~TablePermission.S;
			}
			if ((parent.Stores & TablePermission.DelegateSelect) != TablePermission.DelegateSelect)
			{
				child.Stores &= ~TablePermission.S;
			}

			// INSERT
			if ((parent.Actors & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Actors &= ~TablePermission.I;
			}
			if ((parent.Movies & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Movies &= ~TablePermission.I;
			}
			if ((parent.Categories & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Categories &= ~TablePermission.I;
			}
			if ((parent.Languages & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Languages &= ~TablePermission.I;
			}
			if ((parent.Customers & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Customers &= ~TablePermission.I;
			}
			if ((parent.Staff & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Staff &= ~TablePermission.I;
			}
			if ((parent.Stores & TablePermission.DelegateInsert) != TablePermission.DelegateInsert)
			{
				child.Stores &= ~TablePermission.I;
			}

			// DELETE
			if ((parent.Actors & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Actors &= ~TablePermission.D;
			}
			if ((parent.Movies & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Movies &= ~TablePermission.D;
			}
			if ((parent.Categories & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Categories &= ~TablePermission.D;
			}
			if ((parent.Languages & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Languages &= ~TablePermission.D;
			}
			if ((parent.Customers & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Customers &= ~TablePermission.D;
			}
			if ((parent.Staff & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Staff &= ~TablePermission.D;
			}
			if ((parent.Stores & TablePermission.DelegateDelete) != TablePermission.DelegateDelete)
			{
				child.Stores &= ~TablePermission.D;
			}

			// UPDATE
			if ((parent.Actors & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Actors &= ~TablePermission.U;
			}
			if ((parent.Movies & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Movies &= ~TablePermission.U;
			}
			if ((parent.Categories & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Categories &= ~TablePermission.U;
			}
			if ((parent.Languages & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Languages &= ~TablePermission.U;
			}
			if ((parent.Customers & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Customers &= ~TablePermission.U;
			}
			if ((parent.Staff & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Staff &= ~TablePermission.U;
			}
			if ((parent.Stores & TablePermission.DelegateUpdate) != TablePermission.DelegateUpdate)
			{
				child.Stores &= ~TablePermission.U;
			}

			if (!parent.CanCreateUsers)
				child.CanCreateUsers = false;

			if (!parent.CanTakeOver)
				child.CanTakeOver = false;
		}

		// GET: Permissions/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var permission = await _context.Permissions
				.Include(p => p.User)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (permission == null)
			{
				return NotFound();
			}

			return View(permission);
		}

		// POST: Permissions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.Id == id);
			_context.Permissions.Remove(permission);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool PermissionExists(int id)
		{
			return _context.Permissions.Any(e => e.Id == id);
		}
	}
}
