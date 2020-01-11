using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index(Booking booking)
		{
			if (booking.Date == DateTime.MinValue)
			{
				booking.Date = DateTime.Today;
			}

			return View(booking);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Accessories()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}

		[HttpPost]
		public IActionResult Booking([Bind("Date")] Booking booking)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Index), booking);
			}

			return RedirectToAction(nameof(BookingsController.Index), "Bookings");
		}
	}
}