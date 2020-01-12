using System.Collections.Generic;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Validators;
using Xunit;

namespace UnitTests
{
    public class DiscountValidatorTest
    {
        [Fact]
        private void AlphabeticalDiscount_ReturnsZeroDiscount_WhenAnimalsIsNull()
        {
            //Arrange
            List<Animal> animal = null;
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.GetAlphabeticalDiscount(animal);

            //Assert
            var resultInt = Assert.IsType<int>(result);
            Assert.Equal(0, resultInt);
        }

        [Fact]
        private void AlphabeticalDiscount_ReturnsZeroDiscount_WhenAnimalsAreZero()
        {
            //Arrange
            List<Animal> animals = new List<Animal>();
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.GetAlphabeticalDiscount(animals);

            //Assert
            var resultInt = Assert.IsType<int>(result);
            Assert.Equal(0, resultInt);
        }

        [Fact]
        private void AlphabeticalDiscount_ReturnsDiscount_WhenAnimalsIsValid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.GetAlphabeticalDiscount(animals);

            //Assert
            var resultInt = Assert.IsType<int>(result);
            Assert.Equal(6, resultInt);
        }


        [Fact]
        private void AlphabeticalDiscount_ReturnsZeroDiscount_WhenNotDuck()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var discountValidator = new DiscountValidator();

            //Act
            var isDuck = discountValidator.AnimalsHasDuck(animals);
            var result = discountValidator.ChanceOnDuckDiscount(animals);
            

            //Assert
            var resultInt = Assert.IsType<int>(result);
            Assert.Equal(6, resultInt);
        }



        private List<Animal> GetAnimals()
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
                },
                new Animal()
                {
                ID = 4,
                Name = "Abc",
                Type = AnimalTypes.Jungle,
                Price = 5.50,
                PicturePath = "/images/animals/slang.png"
                }
            };

            return animals;
        }
    }
}