using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ViewComponents
{
	public class BookingDetailViewComponent : ViewComponent
	{
		private readonly BookingDBRepository _repository;

		public BookingDetailViewComponent(IRepository<Booking> repository)
		{
			_repository = (BookingDBRepository)repository;
		}

		public async Task<IViewComponentResult> InvokeAsync(BookingProcess data)
		{
			return View(data);
		}
	}
}
