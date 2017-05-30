using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACManager.Data;
using DACManager.Models;
using Microsoft.AspNetCore.Identity;

namespace DACManager.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public CategoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Categories
		public async Task<IActionResult> Index(string searchString)
		{
			var categories = from c in _context.Categories
							 select c;
			if (!string.IsNullOrEmpty(searchString))
			{
				categories = categories.Where(c => c.Name.Contains(searchString));
			}

			var user = await _userManager.GetUserAsync(HttpContext.User);
			//if (user == null) return Unauthorized();

			user.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == user.Id);

			if (user.Permission.Categories == TablePermission.None) return Unauthorized();

			if ((user.Permission.Categories & TablePermission.Select) == TablePermission.Select)
			{
				ViewData["Select"] = true;
			}
			//else return Unauthorized();

			if ((user.Permission.Categories & TablePermission.Insert) == TablePermission.Insert)
			{
				ViewData["Insert"] = true;
			}

			if ((user.Permission.Categories & TablePermission.Delete) == TablePermission.Delete)
			{
				ViewData["Delete"] = true;
			}

			if ((user.Permission.Categories & TablePermission.Update) == TablePermission.Update)
			{
				ViewData["Update"] = true;
			}
			return View(await categories.ToListAsync());
		}

		// GET: Categories/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var category = await _context.Categories
				.SingleOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		// GET: Categories/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Categories/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Add(category);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(category);
		}

		// GET: Categories/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		// POST: Categories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
		{
			if (id != category.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(category);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryExists(category.Id))
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
			return View(category);
		}

		// GET: Categories/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var category = await _context.Categories
				.SingleOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		// POST: Categories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool CategoryExists(int id)
		{
			return _context.Categories.Any(e => e.Id == id);
		}
	}
}
