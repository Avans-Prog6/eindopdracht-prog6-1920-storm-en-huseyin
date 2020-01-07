using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AccessoriesController : Controller
    {
        private readonly BeestjeOpJeFeestjeContext _context;

        public AccessoriesController(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accessorieses.ToListAsync());
        }

        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessorieses
                .FirstOrDefaultAsync(m => m.ID == id);
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
            if (ModelState.IsValid)
            {
                _context.Add(accessories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessories);
        }

        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessorieses.FindAsync(id);
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
            if (id != accessories.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoriesExists(accessories.ID))
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
            return View(accessories);
        }

        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessories = await _context.Accessorieses
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var accessories = await _context.Accessorieses.FindAsync(id);
            _context.Accessorieses.Remove(accessories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoriesExists(int id)
        {
            return _context.Accessorieses.Any(e => e.ID == id);
        }
    }
}
