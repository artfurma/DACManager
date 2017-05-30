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
	public class LanguagesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public LanguagesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Languages
		public async Task<IActionResult> Index(string searchString)
		{
			var languages = from l in _context.Languages
							select l;

			if (!string.IsNullOrEmpty(searchString))
			{
				languages = languages.Where(l => l.Name.Contains(searchString));
			}

			var user = await _userManager.GetUserAsync(HttpContext.User);
			user.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == user.Id);

			if (user.Permission.Languages == TablePermission.None) return Unauthorized();

			if ((user.Permission.Languages & TablePermission.Select) == TablePermission.Select)
			{
				ViewData["Select"] = true;
			}

			if ((user.Permission.Languages & TablePermission.Insert) == TablePermission.Insert)
			{
				ViewData["Insert"] = true;
			}

			if ((user.Permission.Languages & TablePermission.Delete) == TablePermission.Delete)
			{
				ViewData["Delete"] = true;
			}

			if ((user.Permission.Languages & TablePermission.Update) == TablePermission.Update)
			{
				ViewData["Update"] = true;
			}

			return View(await languages.ToListAsync());
		}

		// GET: Languages/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var language = await _context.Languages
				.SingleOrDefaultAsync(m => m.Id == id);
			if (language == null)
			{
				return NotFound();
			}

			return View(language);
		}

		// GET: Languages/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Languages/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Language language)
		{
			if (ModelState.IsValid)
			{
				_context.Add(language);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(language);
		}

		// GET: Languages/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var language = await _context.Languages.SingleOrDefaultAsync(m => m.Id == id);
			if (language == null)
			{
				return NotFound();
			}
			return View(language);
		}

		// POST: Languages/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Language language)
		{
			if (id != language.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(language);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LanguageExists(language.Id))
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
			return View(language);
		}

		// GET: Languages/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var language = await _context.Languages
				.SingleOrDefaultAsync(m => m.Id == id);
			if (language == null)
			{
				return NotFound();
			}

			return View(language);
		}

		// POST: Languages/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var language = await _context.Languages.SingleOrDefaultAsync(m => m.Id == id);
			_context.Languages.Remove(language);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool LanguageExists(int id)
		{
			return _context.Languages.Any(e => e.Id == id);
		}
	}
}
