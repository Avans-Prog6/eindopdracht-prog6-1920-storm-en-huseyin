using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_ValidatorReturnIndexWithNewBooking_WhenBookingIsNull()
        {
            // Arrange
            var controller = new HomeController();
            //Act
            var result = controller.Index(null);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotEmpty(redirectToActionResult.RouteValues);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Index_ReturnView_WhenBookingIsValid()
        {
            // Arrange
            var controller = new HomeController();
            //Act
            var result = controller.Index(new Booking());

            //Assert
            var actionResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Booking>(actionResult.Model);
        }

        /*[Fact]
        public void Booking_RedirectToIndex_WhenBookingIsInvalid()
        {
            // Arrange
            var controller = new HomeController();

            //Act
            var result = controller.Booking(new Booking());
            controller.ModelState.AddModelError("Date", "Date is not valid!");
            
            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotEmpty(redirectToActionResult.RouteValues);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }*/

        [Fact]
        public void Booking_RedirectToEdit_WhenBookingIsValid()
        {
            // Arrange
            var controller = new HomeController();

            //Act
            var result = controller.Booking(new Booking());
            controller.ModelState.Clear();

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotEmpty(redirectToActionResult.RouteValues);
            Assert.Equal("Bookings", redirectToActionResult.ControllerName);
            Assert.Equal("AnimalSelection", redirectToActionResult.ActionName);
        }

    }
}