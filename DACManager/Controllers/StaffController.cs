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
	public class StaffController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public StaffController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Staff
		public async Task<IActionResult> Index(string searchString)
		{
			var staff = from s in _context.Staff
						select s;

			if (!string.IsNullOrEmpty(searchString))
			{
				staff = staff.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
			}

			var user = await _userManager.GetUserAsync(HttpContext.User);
			user.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == user.Id);

			if (user.Permission.Staff == TablePermission.None) return Unauthorized();

			if ((user.Permission.Staff & TablePermission.Select) == TablePermission.Select)
			{
				ViewData["Select"] = true;
			}

			if ((user.Permission.Staff & TablePermission.Insert) == TablePermission.Insert)
			{
				ViewData["Insert"] = true;
			}

			if ((user.Permission.Staff & TablePermission.Delete) == TablePermission.Delete)
			{
				ViewData["Delete"] = true;
			}

			if ((user.Permission.Staff & TablePermission.Update) == TablePermission.Update)
			{
				ViewData["Update"] = true;
			}

			var applicationDbContext = staff.Include(s => s.Store);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Staff/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var staff = await _context.Staff
				.Include(s => s.Store)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (staff == null)
			{
				return NotFound();
			}

			return View(staff);
		}

		// GET: Staff/Create
		public IActionResult Create()
		{
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
			return View();
		}

		// POST: Staff/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,Email,StoreId")] Staff staff)
		{
			if (ModelState.IsValid)
			{
				_context.Add(staff);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", staff.StoreId);
			return View(staff);
		}

		// GET: Staff/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var staff = await _context.Staff.SingleOrDefaultAsync(m => m.Id == id);
			if (staff == null)
			{
				return NotFound();
			}
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", staff.StoreId);
			return View(staff);
		}

		// POST: Staff/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,Email,StoreId")] Staff staff)
		{
			if (id != staff.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(staff);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StaffExists(staff.Id))
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
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", staff.StoreId);
			return View(staff);
		}

		// GET: Staff/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var staff = await _context.Staff
				.Include(s => s.Store)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (staff == null)
			{
				return NotFound();
			}

			return View(staff);
		}

		// POST: Staff/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var staff = await _context.Staff.SingleOrDefaultAsync(m => m.Id == id);
			_context.Staff.Remove(staff);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool StaffExists(int id)
		{
			return _context.Staff.Any(e => e.Id == id);
		}
	}
}
