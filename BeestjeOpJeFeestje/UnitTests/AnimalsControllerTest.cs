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
            A.CallTo(() => env.WebRootPath).Returns(Directory.GetCurrentDirectory() + "\\wwwroot");

            AnimalsController controller = new AnimalsController(animals, env);

            //Act
            ViewResult result = await controller.Index() as ViewResult;
            
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Animal>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count);

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
                    PicturePath = "images/animals/aap.png"
                },
                new Animal()
                {
                    ID = 2,
                    Name = "Storm",
                    Type = AnimalTypes.Sneeuw,
                    Price = 3.00,
                    PicturePath = "images/animals/ijsbeer.png"
                },
                new Animal()
                {
                    ID = 3,
                    Name = "Caspar",
                    Type = AnimalTypes.Boerderij,
                    Price = 5.50,
                    PicturePath = "images/animals/duck.png"
                }
            };

            return Task.Run(() => animals.ToList());
        }

    }
}
