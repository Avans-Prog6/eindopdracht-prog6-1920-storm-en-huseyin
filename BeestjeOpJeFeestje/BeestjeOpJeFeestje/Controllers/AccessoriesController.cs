using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeestjeOpJeFeestje.Controllers
{
	public class AccessoriesController : Controller
	{
		private readonly IRepository<Accessories> _repository;
		private readonly IWebHostEnvironment _env;

		public AccessoriesController(IRepository<Accessories> repository, IWebHostEnvironment environment)
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
			GetSelectListImages();

			return View();
		}

		private void GetSelectListImages()
		{
			string path = _env.WebRootPath + "/images/accessories/";

			List<SelectListItem> files = new List<SelectListItem>();
			foreach (string filePath in Directory.GetFiles(path))
			{
				SelectListItem fileData = new SelectListItem()
				{
					Text = Path.GetFileNameWithoutExtension(filePath),
					Value = "/images/accessories/" + Path.GetFileName(filePath)
				};

				files.Add(fileData);
			}

			ViewData.Add("files", files);
			ViewData.Add("SelectedFile", "");
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

			GetSelectListImages();

			return View(accessories);
		}

		// POST: Accessories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,PicturePath")] Accessories accessories)
		{
			if (id != accessories.ID)
			{
				return NotFound();
			}

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