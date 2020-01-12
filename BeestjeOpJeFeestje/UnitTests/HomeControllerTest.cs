using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;

namespace UnitTests
{
    public class HomeControllerTest
    {
        public async Task Index_ValidatorReturnError_WhenDateTimeIsInvalid()
        {
            // Arrange
            var booking = new Booking();
            var logger =  ILogger<HomeController>();
            var controller = new HomeController();
            //Act

            //Asssert

        }

    }
}