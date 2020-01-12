using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
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
		private readonly IRepository<Accessories> _accessoriesRepository;
        private readonly IRepository<Animal> _animalRepository;

		private readonly IWebHostEnvironment _env;

		public AccessoriesController(IRepository<Accessories> accessoriesRepository, IRepository<Animal> animalRepository, IWebHostEnvironment environment)
		{
			_accessoriesRepository = accessoriesRepository;
            _animalRepository = animalRepository;
			_env = environment;
		}

		// GET: Accessories
		public async Task<IActionResult> Index()
        {
            List<Animal> animals = await _animalRepository.GetAll();

            if (animals == null) return NotFound();

            List<Accessories> accessories = await _accessoriesRepository.GetAll();

            if (accessories == null) return NotFound();

			ViewData.Add("animals", animals);

			return View(accessories);
		}

		// GET: Accessories/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Accessories accessories = await _accessoriesRepository.Get(id);
			if (accessories == null)
			{
				return NotFound();
			}

			return View(accessories);
		}

		// GET: Accessories/Create
		public async Task<IActionResult> Create()
		{
			AddAccessoryImagesToView();
            await AddAnimalsToView();

            return View();
		}

        private async Task AddAnimalsToView()
        {
            try
            {
                List<Animal> animals = await _animalRepository.GetAll();
                List<SelectListItem> selectedAnimals = new List<SelectListItem>();

                foreach (Animal animal in animals)
                {
                    selectedAnimals.Add(new SelectListItem(animal.Name, animal.ID.ToString()));
                }

                ViewBag.Animals = selectedAnimals;
			}
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
		}

		private void AddAccessoryImagesToView()
		{
            try
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
			}
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
		}

		// POST: Accessories/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID, Name, Discount, PicturePath, AnimalId")] Accessories accessories)
		{
			if (!ModelState.IsValid) return View(accessories);

			await _accessoriesRepository.Create(accessories);
			return RedirectToAction(nameof(Index));
		}

		// GET: Accessories/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Accessories accessories = await _accessoriesRepository.Get(id);

			if (accessories == null)
			{
				return NotFound();
			}

			AddAccessoryImagesToView();
            await AddAnimalsToView();

			return View(accessories);
		}

		// POST: Accessories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID, Name, Discount, PicturePath, AnimalId")] Accessories accessories)
		{
			if (id != accessories.ID)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) return View(accessories);

			try
			{
				await _accessoriesRepository.Update(accessories);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_accessoriesRepository.Exists(accessories.ID))
				{
					return NotFound();
				}

                throw;
				
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

			Accessories accessories = await _accessoriesRepository.Get(id);
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
			Accessories accessories = await _accessoriesRepository.Get(id);
            
            if (accessories != null)
            {
                await _accessoriesRepository.Delete(accessories);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}