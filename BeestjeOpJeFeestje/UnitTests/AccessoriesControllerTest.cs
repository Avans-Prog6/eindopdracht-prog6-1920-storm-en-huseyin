using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class AccessoriesControllerTest
    {
        [Fact]
        public async Task Index_ReturnsIndexResult_WithAListOfAccessories()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            A.CallTo(() => animalsRepo.GetAll()).Returns(GetTestAnimals());
            A.CallTo(() => accessoriesRepo.GetAll()).Returns(GetTestAccessories());

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Index() as ViewResult;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Accessories>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count);

        }

        [Fact]
        public async Task Index_ReturnsNotFound_WhenAnimalListIsNull()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            List<Animal> animals = null;

            A.CallTo(() => animalsRepo.GetAll()).Returns(animals);

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Index();

            //Assert
            A.CallTo(() => animalsRepo.GetAll()).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Index_ReturnsNotFound_WhenAccessoryListIsNull()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            List<Accessories> accessories = null;

            A.CallTo(() => animalsRepo.GetAll()).Returns(GetTestAnimals());

            A.CallTo(() => accessoriesRepo.GetAll()).Returns(accessories);

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Index();

            //Assert
            A.CallTo(() => animalsRepo.GetAll()).MustHaveHappened();
            A.CallTo(() => accessoriesRepo.GetAll()).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Index_ReturnsNotFoundException_WhenAnimalsAreNull()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            List<Animal> animal = null;

            A.CallTo(() => animalsRepo.GetAll()).Returns(animal);

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Index();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsAnimalViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            controller.ModelState.AddModelError("Name", "Required");

            var accessory = GetAccessory();

            //Act
            var result = await controller.Create(accessory);

            //Assert
            var accessoryResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Accessories>(accessoryResult.Model);
        }

        [Fact]
        public async Task Create_AddsAnimalAndRedirectsToIndex_WhenModelStateIsValid()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            var accessory = GetAccessory();

            //Act
            var result = await controller.Create(accessory);

            //Assert
            A.CallTo(() => accessoriesRepo.Create(A<Accessories>.Ignored)).MustHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task Edit_ReturnsAnimalViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            controller.ModelState.AddModelError("Name", "Required");

            var accessory = GetAccessory();

            //Act
            var result = await controller.Edit(accessory.ID, accessory);

            //Assert
            var animalResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Accessories>(animalResult.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchWithAnimal()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            var accessory = GetAccessory();
            //Act
            var result = await controller.Edit(-1, accessory);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Edit_EditsAnimalAndReturnsToIndex_WhenAnimalAndIdAreValid()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            var accessory = GetAccessory();

            //Act
            var result = await controller.Edit(accessory.ID, accessory);

            //Assert
            A.CallTo(() => accessoriesRepo.Update(A<Accessories>.Ignored)).MustHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenDoesNotExist()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            var accessory = GetAccessory();

            A.CallTo(() => accessoriesRepo.Update(A<Accessories>.Ignored)).Throws(new DbUpdateConcurrencyException());
            A.CallTo(() => accessoriesRepo.Exists(A<int>.Ignored)).Returns(false);

            //Act
            var result = await controller.Edit(4, accessory);

            //Assert
            A.CallTo(() => accessoriesRepo.Exists(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task EditGet_ReturnNotFoundException_WhenAnimalIdIsNull()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Edit(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task EditGet_ReturnNotFoundException_WhenAnimalsIsNull()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            Accessories accessories = null;

            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).Returns(accessories);

            //Act
            var result = await controller.Edit(1);

            //Assert
            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditGet_ReturnsViewData_WhenAnimalIsValid()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            Accessories accessory = GetAccessory();

            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).Returns(accessory);

            //Act
            var result = await controller.Edit(1);

            //Assert
            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).MustHaveHappened();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Accessories>(viewResult.Model);

        }


        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            //Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Details(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenAnimalsReturnsNull()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            Accessories accessory = null;

            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).Returns(accessory);

            //Act
            var result = await controller.Details(-1);

            //Assert
            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_AnimalReturnsToIndex_WhenIdIsValid()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            A.CallTo(() => accessoriesRepo.Get(A<int>.That.IsEqualTo(1))).Returns(GetAccessory());

            //Act
            var result = await controller.Details(1);

            //Assert
            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).MustHaveHappened();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(viewResult.Model);
            Assert.IsType<Accessories>(viewResult.Model);
        }

        [Fact]
        public async Task Delete_ReturnNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            //Act
            var result = await controller.Delete(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Delete_AnimalReturnsNull_WhenIdIsInvalid()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            Accessories accessory = null;

            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).Returns(accessory);

            //Act
            var result = await controller.Delete(-1);

            //Assert
            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_AnimalReturnsToIndex_WhenIdIsValid()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            A.CallTo(() => accessoriesRepo.Get(A<int>.That.IsEqualTo(4))).Returns(GetAccessory());

            //Act
            var result = await controller.Delete(4);

            //Assert
            A.CallTo(() => accessoriesRepo.Get(A<int>.Ignored)).MustHaveHappened();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(viewResult.Model);
            Assert.IsType<Accessories>(viewResult.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsIndex_WhenAnimalsIsNull()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);

            Accessories accessory = null;

            A.CallTo(() => accessoriesRepo.Get(1)).Returns(accessory);

            //Act
            var result = await controller.DeleteConfirmed(1);

            //Assert
            A.CallTo(() => accessoriesRepo.Delete(accessory)).MustNotHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_DeleteAnimalsAndReturnsIndex_WhenAnimalIsValid()
        {
            // Arrange
            var accessoriesRepo = A.Fake<IRepository<Accessories>>();
            var animalsRepo = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AccessoriesController(accessoriesRepo, animalsRepo, env);
            Accessories accessory = GetAccessory();

            A.CallTo(() => accessoriesRepo.Get(1)).Returns(accessory);

            //Act
            var result = await controller.DeleteConfirmed(1);

            //Assert
            A.CallTo(() => accessoriesRepo.Delete(accessory)).MustHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        private Accessories GetAccessory()
        {
            return new Accessories
            {
                ID = 4,
                Name = "Kerstmuts",
                PicturePath = "/images/accessories/Picture 4.png",
                Price = 25.0,
                AnimalId = 3
            };
        }

        private Task<List<Accessories>> GetTestAccessories()
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

            return Task.Run(() => accessories.ToList());
        }

        private Task<List<Animal>> GetTestAnimals()
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

            return Task.Run(() => animals.ToList());
        }

    }
}

