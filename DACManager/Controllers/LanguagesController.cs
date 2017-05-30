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
	public class LanguagesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public LanguagesController(ApplicationDbContext context)
		{
			_context = context;
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
