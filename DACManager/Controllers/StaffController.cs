using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACManager.Data;
using DACManager.Models;

namespace DACManager.Controllers
{
	public class StaffController : Controller
	{
		private readonly ApplicationDbContext _context;

		public StaffController(ApplicationDbContext context)
		{
			_context = context;
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
