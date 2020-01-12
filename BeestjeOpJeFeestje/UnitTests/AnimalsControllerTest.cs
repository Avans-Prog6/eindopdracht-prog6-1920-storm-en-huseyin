using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class AnimalsControllerTest
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAnimals()
        {
            //Arrange
            var animals = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            A.CallTo(() => animals.GetAll()).Returns(GetTestAnimals());

            var controller = new AnimalsController(animals, env);

            //Act
            var result = await controller.Index() as ViewResult;
            
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Animal>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task Index_ReturnsNotFound_WhenListIsNull()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            List<Animal> animals = null;

            A.CallTo(() => repository.GetAll()).Returns(animals);

            var controller = new AnimalsController(repository, env);

            //Act
            var result = await controller.Index();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsAnimalViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            controller.ModelState.AddModelError("Name", "Required");

            var animal = GetAnimal();

            //Act
            var result = await controller.Create(animal);

            //Assert
            var animalResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Animal>(animalResult.Model);
        }

        [Fact]
        public async Task Create_AddsAnimalAndRedirectsToIndex_WhenModelStateIsValid()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            var animal = GetAnimal();

            //Act
            var result = await controller.Create(animal);

            //Assert
            A.CallTo(() => repository.Create(A<Animal>.Ignored)).MustHaveHappened();
           
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task Edit_ReturnsAnimalViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            controller.ModelState.AddModelError("Name", "Required");

            var animal = GetAnimal();

            //Act
            var result = await controller.Edit(animal.ID, animal);

            //Assert
            var animalResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Animal>(animalResult.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenIdDoesNotMatchWithAnimal()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            var animal = GetAnimal();
            //Act
            var result = await controller.Edit(-1, animal);
            
            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Edit_ReturnsNotFoundResult_WhenDoesNotExist()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            var animal = GetAnimal();

            A.CallTo(() => repository.Update(A<Animal>.Ignored)).Throws(new DbUpdateConcurrencyException());
            A.CallTo(() => repository.Exists(A<int>.Ignored)).Returns(false);

            //Act
            var result = await controller.Edit(1, animal);

            //Assert
            A.CallTo(() => repository.Exists(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Edit_EditsAnimalAndReturnsToIndex_WhenAnimalAndIdAreValid()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            var animal = GetAnimal();

            //Act
            var result = await controller.Edit(animal.ID, animal);

            //Assert
            A.CallTo(() => repository.Update(A<Animal>.Ignored)).MustHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task EditGet_ReturnNotFoundException_WhenAnimalIdIsNull()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);

            //Act
            var result = await controller.Edit(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task EditGet_ReturnNotFoundException_WhenAnimalsIsNull()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            Animal animal = null;

            A.CallTo(() => repository.Get(A<int>.Ignored)).Returns(animal);

            //Act
            var result = await controller.Edit(1);

            //Assert
            A.CallTo(() => repository.Get(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task EditGet_ReturnsViewData_WhenAnimalIsValid()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            Animal animal = GetAnimal();

            A.CallTo(() => repository.Get(A<int>.Ignored)).Returns(animal);

            //Act
            var result = await controller.Edit(1);

            //Assert
            A.CallTo(() => repository.Get(A<int>.Ignored)).MustHaveHappened();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Animal>(viewResult.Model);

        }

        [Fact]
        public async Task Delete_ReturnNotFoundResult_WhenIdIsNull()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);

            //Act
            var result = await controller.Delete(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenAnimalsReturnsNull()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            Animal animal = null;

            A.CallTo(() => repository.Get(A<int>.Ignored)).Returns(animal);

            //Act
            var result = await controller.Delete(-1);

            //Assert
            A.CallTo(() => repository.Get(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_AnimalReturnsToIndex_WhenIdIsValid()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            A.CallTo(() => repository.Get(A<int>.That.IsEqualTo(1))).Returns(GetAnimal());

            //Act
            var result = await controller.Delete(1);

            //Assert
            A.CallTo(() => repository.Get(A<int>.Ignored)).MustHaveHappened();
            
            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(viewResult.Model);
            Assert.IsType<Animal>(viewResult.Model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            //Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            
            //Act
            var result = await controller.Details(null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenAnimalsReturnsNull()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            Animal animal = null;

            A.CallTo(() => repository.Get(A<int>.Ignored)).Returns(animal);

            //Act
            var result = await controller.Details(-1);

            //Assert
            A.CallTo(() => repository.Get(A<int>.Ignored)).MustHaveHappened();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_AnimalReturnsToIndex_WhenIdIsValid()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            A.CallTo(() => repository.Get(A<int>.That.IsEqualTo(1))).Returns(GetAnimal());

            //Act
            var result = await controller.Details(1);

            //Assert
            A.CallTo(() => repository.Get(A<int>.Ignored)).MustHaveHappened();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(viewResult.Model);
            Assert.IsType<Animal>(viewResult.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsIndex_WhenAnimalsIsNull()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);

            Animal animal = null;

            A.CallTo(() => repository.Get(1)).Returns(animal);
            
            //Act
            var result = await controller.DeleteConfirmed(1);

            //Assert
            A.CallTo(() => repository.Delete(animal)).MustNotHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteConfirmed_DeleteAnimalsAndReturnsIndex_WhenAnimalIsValid()
        {
            // Arrange
            var repository = A.Fake<IRepository<Animal>>();
            var env = A.Fake<IWebHostEnvironment>();

            var controller = new AnimalsController(repository, env);
            Animal animal = GetAnimal();

            A.CallTo(() => repository.Get(1)).Returns(animal);

            //Act
            var result = await controller.DeleteConfirmed(1);

            //Assert
            A.CallTo(() => repository.Delete(animal)).MustHaveHappened();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        private Animal GetAnimal()
        {
            return new Animal()
            {
                ID = 1,
                Name = "Huseyin",
                Type = AnimalTypes.Woestijn,
                Price = 45.00,
                PicturePath = "/images/animals/aap.png"
            };
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
