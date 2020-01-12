using System;
using System.Collections.Generic;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Validators;
using Xunit;

namespace UnitTests
{
    public class DiscountValidatorTest
    {
        [Fact]
        private void AlphabeticalDiscount_ReturnsZeroDiscount_WhenAnimalsAreNull()
        {
            //Arrange
            List<Animal> animals = null;
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.GetAlphabeticalDiscount(animals);

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
        private void AlphabeticalDiscount_ReturnsDiscount_WhenAnimalsAreValid()
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
        private void ChanceOnDuckDiscount_ReturnsZeroDiscount_WhenNotDuck()
        {
            //Arrange
            List<Animal> animals = GetAnimalsNoDuck();
            var discountValidator = new DiscountValidator();

            //Act
            var isDuck = discountValidator.AnimalsHasDuck(animals);
            var result = discountValidator.ChanceOnDuckDiscount(animals);


            //Assert
            var intResult = Assert.IsType<int>(result);
            Assert.Equal(0, intResult);
            Assert.False(isDuck);
        }

        [Fact]
        private void ChanceOnDuckDiscount_ReturnsZeroDiscount_WhenAnimalsNull()
        {
            //Arrange
            List<Animal> animals = null;
            var discountValidator = new DiscountValidator();

            //Act
            var isDuck = discountValidator.AnimalsHasDuck(animals);
            var result = discountValidator.ChanceOnDuckDiscount(animals);


            //Assert
            var intResult = Assert.IsType<int>(result);
            Assert.Equal(0, intResult);
            Assert.False(isDuck);
        }


        [Fact]
        private void ChanceOnDuckDiscount_ReturnsDiscount_WhenDuck()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var discountValidator = new DiscountValidator();

            //Act
            var isDuck = discountValidator.AnimalsHasDuck(animals);
            var result = discountValidator.ChanceOnDuckDiscount(animals);
            

            //Assert
            Assert.IsType<int>(result);
            Assert.True(isDuck);
        }

        [Fact]
        private void BookingIsMondayOrTuesday_ReturnsZeroDiscount_WhenInvalidDate()
        {
            //Arrange
            DateTime dateTime = new DateTime(2020, 01, 17);
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.BookingIsMondayOrTuesday(dateTime);


            //Assert
            var intResult = Assert.IsType<int>(result);

            Assert.NotEqual(DayOfWeek.Monday, dateTime.DayOfWeek);
            Assert.NotEqual(DayOfWeek.Tuesday, dateTime.DayOfWeek);
            Assert.Equal(0, intResult);
        }


        [Fact]
        private void BookingIsMondayOrTuesday_ReturnsDiscount_WhenValidDate()
        {
            //Arrange
            DateTime dateTime = new DateTime(2020, 01, 14);
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.BookingIsMondayOrTuesday(dateTime);


            //Assert
            var intResult = Assert.IsType<int>(result);
            Assert.Equal(DayOfWeek.Tuesday, dateTime.DayOfWeek);
            Assert.Equal(15, intResult);
        }

        [Fact]
        private void GetMaximumPercentage_ReturnsMaxDiscount_WhenOverMaxDiscount()
        {
            //Arrange
            int discountPercentage = 70;
            var discountValidator = new DiscountValidator();

            //Act
            var result = discountValidator.GetMaximumPercentage(discountPercentage, 60);

            //Assert
            var intResult = Assert.IsType<int>(result);
            Assert.Equal(60, intResult);
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
                    Name = "Eend",
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

        private List<Animal> GetAnimalsNoDuck()
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