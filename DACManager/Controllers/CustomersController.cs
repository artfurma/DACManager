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
	public class CustomersController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;


		public CustomersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;

		}

		// GET: Customers
		public async Task<IActionResult> Index(string searchString)
		{
			var customers = from c in _context.Customers
							select c;

			if (string.IsNullOrEmpty(searchString))
			{
				customers = customers.Where(c => c.FirstName.Contains(searchString) || c.LastName.Contains(searchString));
			}

			var user = await _userManager.GetUserAsync(HttpContext.User);
			user.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == user.Id);

			if (user.Permission.Customers == TablePermission.None) return Unauthorized();

			if ((user.Permission.Customers & TablePermission.Select) == TablePermission.Select)
			{
				ViewData["Select"] = true;
			}

			if ((user.Permission.Customers & TablePermission.Insert) == TablePermission.Insert)
			{
				ViewData["Insert"] = true;
			}

			if ((user.Permission.Customers & TablePermission.Delete) == TablePermission.Delete)
			{
				ViewData["Delete"] = true;
			}

			if ((user.Permission.Customers & TablePermission.Update) == TablePermission.Update)
			{
				ViewData["Update"] = true;
			}

			var applicationDbContext = customers.Include(c => c.Store);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Customers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var customer = await _context.Customers
				.Include(c => c.Store)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (customer == null)
			{
				return NotFound();
			}

			return View(customer);
		}

		// GET: Customers/Create
		public IActionResult Create()
		{
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id");
			return View();
		}

		// POST: Customers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,Email,StoreId")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				_context.Add(customer);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", customer.StoreId);
			return View(customer);
		}

		// GET: Customers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
			if (customer == null)
			{
				return NotFound();
			}
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", customer.StoreId);
			return View(customer);
		}

		// POST: Customers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,Email,StoreId")] Customer customer)
		{
			if (id != customer.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(customer);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CustomerExists(customer.Id))
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
			ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Id", customer.StoreId);
			return View(customer);
		}

		// GET: Customers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var customer = await _context.Customers
				.Include(c => c.Store)
				.SingleOrDefaultAsync(m => m.Id == id);
			if (customer == null)
			{
				return NotFound();
			}

			return View(customer);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool CustomerExists(int id)
		{
			return _context.Customers.Any(e => e.Id == id);
		}
	}
}
