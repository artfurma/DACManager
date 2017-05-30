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
	public class MoviesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public MoviesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Movies
		public async Task<IActionResult> Index(string searchString)
		{
			var movies = from m in _context.Movies
						 select m;

			if (!string.IsNullOrEmpty(searchString))
			{
				movies = movies.Where(m => m.Title.Contains(searchString));
			}

			var user = await _userManager.GetUserAsync(HttpContext.User);
			user.Permission = _context.Permissions.FirstOrDefault(p => p.UserId == user.Id);

			if (user.Permission.Movies == TablePermission.None) return Unauthorized();

			if ((user.Permission.Movies & TablePermission.Select) == TablePermission.Select)
			{
				ViewData["Select"] = true;
			}

			if ((user.Permission.Movies & TablePermission.Insert) == TablePermission.Insert)
			{
				ViewData["Insert"] = true;
			}

			if ((user.Permission.Movies & TablePermission.Delete) == TablePermission.Delete)
			{
				ViewData["Delete"] = true;
			}

			if ((user.Permission.Movies & TablePermission.Update) == TablePermission.Update)
			{
				ViewData["Update"] = true;
			}

			return View(await movies.ToListAsync());
		}

		// GET: Movies/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var movie = await _context.Movies
				.SingleOrDefaultAsync(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}

			return View(movie);
		}

		// GET: Movies/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Movies/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Length,ReleaseDate")] Movie movie)
		{
			if (ModelState.IsValid)
			{
				_context.Add(movie);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(movie);
		}

		// GET: Movies/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}
			return View(movie);
		}

		// POST: Movies/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Length,ReleaseDate")] Movie movie)
		{
			if (id != movie.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(movie);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MovieExists(movie.Id))
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
			return View(movie);
		}

		// GET: Movies/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var movie = await _context.Movies
				.SingleOrDefaultAsync(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}

			return View(movie);
		}

		// POST: Movies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
			_context.Movies.Remove(movie);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		private bool MovieExists(int id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}
	}
}
