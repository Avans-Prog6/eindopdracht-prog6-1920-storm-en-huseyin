using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using BeestjeOpJeFeestje.ViewComponents;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Xunit;

namespace UnitTests
{
    public class ConfirmationViewTest
    {
        [Fact]
        private async Task InvokeAsync_ReturnDiscount_WhenAnimalsIsSameType()
        {
            //Arrange
            var bookingProcessRepo = A.Fake<IRepository<BookingProcess>>();
            var bookingRepo = A.Fake<IRepository<Booking>>();
            var animalRepo = A.Fake<IRepository<Animal>>();
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var clientInfoRepo = A.Fake<IRepository<ClientInfo>>();

            var confirmationView = new ConfirmationViewComponent(bookingProcessRepo, bookingRepo, animalRepo, accessoriesRepo, clientInfoRepo);

            A.CallTo(() => animalRepo.Find(A<int[]>.Ignored)).Returns(GetAnimalsSameType());

            var bookingProcessData = GetBookingProcess();
            bookingProcessData.Animals = GetAnimalsSameType();

            //Act
            var result = await confirmationView.InvokeAsync(bookingProcessData);

            //Assert
            A.CallTo(() => animalRepo.Find(A<int[]>.Ignored)).MustHaveHappened();
            var viewResult = Assert.IsType<ViewViewComponentResult>(result);
            var bookingProcess = Assert.IsType<BookingProcess>(viewResult.ViewData.Model);
            Assert.Equal(10, bookingProcess.TotalDiscount);
        }


        private BookingProcess GetBookingProcess()
        {
            return new BookingProcess()
            {
                ID = 1,
                Booking = GetBooking(),
                ClientInfoId = 2,
                ClientInfo = GetClientInfo(),
                Animals = GetAnimalsNormal(),
                Accessories = GetAccessories(),
            };
        }

        private ClientInfo GetClientInfo()
        {
            return new ClientInfo()
            {
                ID = 2,
                Address = "Schilfgaarde 32",
                Email = "stormwitziers123@gmail.com",
                FirstName = "Storm",
                MiddleName = "",
                LastName = "Witziers"

            };
        }

        private Booking GetBooking()
        {
            return new Booking()
            {
                ID = 2,
                BookingState = BookingState.Confirmation,
                Date = DateTime.Now
            };
        }

        private List<Accessories> GetAccessories()
        {
            List<Accessories> accessories = new List<Accessories>()
            {
                new Accessories
                {
                    ID = 4,
                    Name = "Kerstmuts",
                    PicturePath = "/images/accessories/Picture 4.png",
                    Price = 25.0,
                    AnimalId = 3
                },
                new Accessories
                {
                    ID = 5,
                    Name = "Maracas",
                    PicturePath = "/images/accessories/Picture 5.png",
                    Price = 10.0,
                    AnimalId = 4
                },
                new Accessories
                {
                    ID = 6,
                    Name = "Hamer",
                    PicturePath = "/images/accessories/Picture 6.png",
                    Price = 3.0,
                    AnimalId = 5
                },
            };

            return accessories;
        }

        private List<Animal> GetAnimalsSameType()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Animal()
                {
                    ID = 1,
                    Name = "Huseyin",
                    Type = AnimalTypes.Woestijn,
                    Price = 45.00,
                    PicturePath = "/images/animals/aap.png"
                },
                new Animal()
                {
                    ID = 2,
                    Name = "Storm",
                    Type = AnimalTypes.Woestijn,
                    Price = 3.00,
                    PicturePath = "/images/animals/ijsbeer.png"
                },
                new Animal()
                {
                    ID = 3,
                    Name = "Caspar",
                    Type = AnimalTypes.Woestijn,
                    Price = 5.50,
                    PicturePath = "/images/animals/duck.png"
                }
            };

            return animals;
        }
        private List<Animal> GetAnimalsNormal()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Animal()
                {
                    ID = 1,
                    Name = "Huseyin",
                    Type = AnimalTypes.Woestijn,
                    Price = 45.00,
                    PicturePath = "/images/animals/aap.png"
                },
                new Animal()
                {
                    ID = 2,
                    Name = "Storm",
                    Type = AnimalTypes.Sneeuw,
                    Price = 3.00,
                    PicturePath = "/images/animals/ijsbeer.png"
                },
                new Animal()
                {
                    ID = 3,
                    Name = "Caspar",
                    Type = AnimalTypes.Boerderij,
                    Price = 5.50,
                    PicturePath = "/images/animals/duck.png"
                }
            };

            return animals;
        }
    }
}