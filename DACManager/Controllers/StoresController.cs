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

namespace DACManager.Controllers
{
	public class StoresController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public StoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Stores
		public async Task<IActionResult> Index(string searchString)
		{
			var stores = from s in _context.Stores
						 select s;

			if (!string.IsNullOrEmpty(searchString))
			{
				stores = stores.Where(s => s.Name.Contains(searchString));
			}

			var user = await _userManager.GetUserAsync(HttpContext.User);
			user.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == user.Id);

			if (user.Permission.Stores == TablePermission.None) return Unauthorized();

			if ((user.Permission.Stores & TablePermission.Select) == TablePermission.Select)
			{
				ViewData["Select"] = true;
			}

			if ((user.Permission.Stores & TablePermission.Insert) == TablePermission.Insert)
			{
				ViewData["Insert"] = true;
			}

			if ((user.Permission.Stores & TablePermission.Delete) == TablePermission.Delete)
			{
				ViewData["Delete"] = true;
			}

			if ((user.Permission.Stores & TablePermission.Update) == TablePermission.Update)
			{
				ViewData["Update"] = true;
			}

			return View(await stores.ToListAsync());
		}

		// GET: Stores/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var store = await _context.Stores
				.SingleOrDefaultAsync(m => m.Id == id);
			if (store == null)
			{
				return NotFound();
			}

			return View(store);
		}

		// GET: Stores/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Stores/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Address")] Store store)
		{
			if (ModelState.IsValid)
			{
				_context.Add(store);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(store);
		}

		// GET: Stores/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var store = await _context.Stores.SingleOrDefaultAsync(m => m.Id == id);
			if (store == null)
			{
				return NotFound();
			}
			return View(store);
		}

		// POST: Stores/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address")] Store store)
		{
			if (id != store.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(store);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StoreExists(store.Id))
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
			return View(store);
		}

		// GET: Stores/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var store = await _context.Stores
				.SingleOrDefaultAsync(m => m.Id == id);
			if (store == null)
			{
				return NotFound();
			}

			return View(store);
		}

		// POST: Stores/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var store = await _context.Stores.SingleOrDefaultAsync(m => m.Id == id);
			_context.Stores.Remove(store);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool StoreExists(int id)
		{
			return _context.Stores.Any(e => e.Id == id);
		}
	}
}
