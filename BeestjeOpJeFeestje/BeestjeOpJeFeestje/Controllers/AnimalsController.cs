using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IRepository<Animal> _repository;

        public AnimalsController(IRepository<Animal> animalRepository)
        {
            _repository = animalRepository;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Animal animal = await _repository.Get(id);
                
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type,Price,PicturePath")] Animal animal)
        {
            if (!ModelState.IsValid) return View(animal);


            await _repository.Create(animal);
            return RedirectToAction(nameof(Index));
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            Animal animal = await _repository.Get(id);
            
            if (animal == null) { return NotFound(); }

            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Type,Price,PicturePath")] Animal animal)
        {
            if (id != animal.ID) { return NotFound(); }
            if (!ModelState.IsValid) return View(animal);

            try
            {
               await _repository.Update(animal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.AnimalExists(animal.ID))
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

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            Animal animal = await _repository.Get(id);
            if (animal == null) { return NotFound(); }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Animal animal = await _repository.Get(id);
            await _repository.Delete(animal);
            return RedirectToAction(nameof(Index));
        }
    }
}
