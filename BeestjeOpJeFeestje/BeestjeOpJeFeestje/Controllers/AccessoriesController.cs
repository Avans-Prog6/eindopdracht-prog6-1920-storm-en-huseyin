using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace BeestjeOpJeFeestje.Controllers
{
	public class AccessoriesController : Controller
	{
		private readonly IRepository<Accessories> _repository;
		private readonly IWebHostEnvironment _env;

		public AccessoriesController(IRepository<Accessories> repository, IWebHostEnvironment  environment)
		{
			_repository = repository;
			_env = environment;
		}

		// GET: Accessories
		public async Task<IActionResult> Index()
		{
			return View(await _repository.GetAll());
		}

		// GET: Accessories/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Accessories accessories = await _repository.Get(id);
			if (accessories == null)
			{
				return NotFound();
			}

			return View(accessories);
		}

		// GET: Accessories/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Accessories/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Name,Price,PicturePath")] Accessories accessories)
		{
			if (!ModelState.IsValid) return View(accessories);


			await _repository.Create(accessories);
			return RedirectToAction(nameof(Index));
		}

		// GET: Accessories/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Accessories accessories = await _repository.Get(id);
			
			if (accessories == null)
			{
				return NotFound();
			}

			return View(accessories);
		}

		// POST: Accessories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,PicturePath")] Accessories accessories)
		{
			if (id != accessories.ID) { return NotFound(); }
			if (!ModelState.IsValid) return View(accessories);

			try
			{
				await _repository.Update(accessories);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_repository.Exists(accessories.ID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return RedirectToAction(nameof(Index));
		}

		// GET: Accessories/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) { return NotFound(); }

			Accessories accessories = await _repository.Get(id);
			if (accessories == null) { return NotFound(); }

			return View(accessories);
		}

		// POST: Accessories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Accessories accessories = await _repository.Get(id);
			await _repository.Delete(accessories);
			return RedirectToAction(nameof(Index));
		}
	}
}