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
	public class ActorsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ActorsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Actors
		public async Task<IActionResult> Index(string searchString)
		{
			var actors = from a in _context.Actors
						 select a;

			if (!string.IsNullOrEmpty(searchString))
			{
				actors = actors.Where(a => a.FirstName.Contains(searchString) || a.LastName.Contains(searchString));
			}

			return View(await actors.ToListAsync());
		}

		[HttpPost]
		public string Index(string searchString, bool notUsed)
		{
			return "From [HttpPost]Index: filter on " + searchString;
		}

		// GET: Actors/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actor = await _context.Actors
				.SingleOrDefaultAsync(m => m.Id == id);
			if (actor == null)
			{
				return NotFound();
			}

			return View(actor);
		}

		// GET: Actors/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Actors/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate")] Actor actor)
		{
			if (ModelState.IsValid)
			{
				_context.Add(actor);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(actor);
		}

		// GET: Actors/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actor = await _context.Actors.SingleOrDefaultAsync(m => m.Id == id);
			if (actor == null)
			{
				return NotFound();
			}
			return View(actor);
		}

		// POST: Actors/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,BirthDate")] Actor actor)
		{
			if (id != actor.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(actor);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ActorExists(actor.Id))
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
			return View(actor);
		}

		// GET: Actors/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var actor = await _context.Actors
				.SingleOrDefaultAsync(m => m.Id == id);
			if (actor == null)
			{
				return NotFound();
			}

			return View(actor);
		}

		// POST: Actors/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var actor = await _context.Actors.SingleOrDefaultAsync(m => m.Id == id);
			_context.Actors.Remove(actor);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool ActorExists(int id)
		{
			return _context.Actors.Any(e => e.Id == id);
		}
	}
}
