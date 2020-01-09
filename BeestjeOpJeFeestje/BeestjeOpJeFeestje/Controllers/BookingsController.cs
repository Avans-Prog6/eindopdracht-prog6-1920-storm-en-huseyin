using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
	public class BookingsController : Controller
	{
		private readonly IRepository<Booking> _repository;

		public BookingsController(IRepository<Booking> repository)
		{
			_repository = repository;
		}

		// GET: Bookings
		public async Task<IActionResult> Index()
		{
			return View(await _repository.GetAll());
		}

		// GET: Bookings/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Booking booking = await _repository.Get(id);

			if (booking == null)
			{
				return NotFound();
			}

			return View(booking);
		}

		// GET: Bookings/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Bookings/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Date")] Booking booking)
		{
			if (!ModelState.IsValid) return View(booking);


			await _repository.Create(booking);
			return RedirectToAction(nameof(Index));
		}

		// GET: Bookings/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Booking booking = await _repository.Get(id);
			if (booking == null)
			{
				return NotFound();
			}

			return View(booking);
		}

		// POST: Bookings/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Date")] Booking booking)
		{
			if (id != booking.ID)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) return View(booking);

			try
			{
				await _repository.Update(booking);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_repository.Exists(booking.ID))
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

		// GET: Bookings/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Booking booking = await _repository.Get(id);
			if (booking == null)
			{
				return NotFound();
			}

			return View(booking);
		}

		// POST: Bookings/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Booking booking = await _repository.Get(id);
			await _repository.Delete(booking);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost, ActionName("AnimalSelection")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AnimalSelection(Booking booking)
		{
			if (!await _repository.Exists(booking))
			{
				return View(booking);
			}

			return RedirectToAction("Index", "Home");
		}
	}
}