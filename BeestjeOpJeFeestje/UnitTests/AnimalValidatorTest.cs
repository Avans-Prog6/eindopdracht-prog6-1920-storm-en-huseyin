using System;
using System.Collections.Generic;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Validators;
using Xunit;

namespace UnitTests
{
    public class AnimalValidatorTest
    {
        [Fact]
        private void FarmAnimalHasNoLionOrIceBear_ThrowNullReferenceException_WhenAnimalsAreNull()
        {
            //Arrange
            List<Animal> animals = null;
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => animalSelectionValidator.FarmAnimalHasNoLionOrIceBear(animals));
        }

        [Fact]
        private void FarmAnimalHasNoLionOrIceBear_ReturnTrue_WhenAnimalsAreZero()
        {
            //Arrange
            List<Animal> animals = new List<Animal>();
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act
            var result = animalSelectionValidator.FarmAnimalHasNoLionOrIceBear(animals);
            
            //Assert
            Assert.True(result);
        }

        [Fact]
        private void FarmAnimalHasNoLionOrIceBear_ReturnFalse_WhenAnimalAreFarmAndPolarBear()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act
            var result = animalSelectionValidator.FarmAnimalHasNoLionOrIceBear(animals);

            //Assert
            Assert.False(result);
        }

        [Fact]
        private void PenguinIsHiredInWeekend_ThrowsNullReferenceException_WhenAnimalsAreNull()
        {
            //Arrange
            List<Animal> animals = null;
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act && Assert
            Assert.Throws<NullReferenceException>(() => animalSelectionValidator.PenguinIsHiredInWeekend(animals, DateTime.Now));

        }


        [Fact]
        private void PenguinIsHiredInWeekend_ReturnFalse_WhenDateIsInvalid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 01, 15);

            //Act
            var restult = animalSelectionValidator.PenguinIsHiredInWeekend(animals, dateTime);

            //Assert
            Assert.False(restult);

        }

        [Fact]
        private void PenguinIsHiredInWeekend_ReturnFalse_WhenPinguinIsFalse()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 01, 18);

            //Act
            var restult = animalSelectionValidator.PenguinIsHiredInWeekend(animals, dateTime);

            //Assert
            Assert.False(restult);

        }

        [Fact]
        private void PenguinIsHiredInWeekend_ReturnTrue_WhenDataIsValid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals.Add(new Animal()
            {
                ID = 4,
                Name = "Pinguin",
                Type = AnimalTypes.Sneeuw,
                Price = 45.00,
                PicturePath = "/images/animals/pingwing.png"
            });
            
            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 01, 18);

            //Act
            var result = animalSelectionValidator.PenguinIsHiredInWeekend(animals, dateTime);

            //Assert
            Assert.True(result);

        }


        [Fact]
        private void SnowAnimalIsHiredForSummer_ThrowsNullReferenceException_WhenAnimalsAreNull()
        {
            //Arrange
            List<Animal> animals = null;
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act && Assert
            Assert.Throws<NullReferenceException>(() => animalSelectionValidator.SnowAnimalIsHiredForSummer(animals, DateTime.Now));

        }

        [Fact]
        private void SnowAnimalIsHiredForSummer_ReturnFalse_WhenDateIsInvalid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals[1].Type = AnimalTypes.Sneeuw;

            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 5, 15);

            //Act
            var result = animalSelectionValidator.SnowAnimalIsHiredForSummer(animals, dateTime);

            //Assert
            Assert.False(result);

        }

        [Fact]
        private void SnowAnimalIsHiredForSummer_ReturnFalse_WhenTypeIsInvalid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals[1].Type = AnimalTypes.Woestijn;

            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 01, 18);

            //Act
            var restult = animalSelectionValidator.SnowAnimalIsHiredForSummer(animals, dateTime);

            //Assert
            Assert.False(restult);

        }

        [Fact]
        private void SnowAnimalIsHiredForSummer_ReturnTrue_WhenDataIsValid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals[1].Type = AnimalTypes.Sneeuw;

            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 7, 1);

            //Act
            var result = animalSelectionValidator.SnowAnimalIsHiredForSummer(animals, dateTime);

            //Assert
            Assert.True(result);

        }

        [Fact]
        private void DesertAnimalIsHiredInWinter_ThrowsNullReferenceException_WhenAnimalsAreNull()
        {
            //Arrange
            List<Animal> animals = null;
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act && Assert
            Assert.Throws<NullReferenceException>(() => animalSelectionValidator.DesertAnimalIsHiredInWinter(animals, DateTime.Now));

        }

        [Fact]
        private void DesertAnimalIsHiredInWinter_ReturnFalse_WhenDateIsInvalid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals[1].Type = AnimalTypes.Woestijn;

            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 5, 15);

            //Act
            var result = animalSelectionValidator.DesertAnimalIsHiredInWinter(animals, dateTime);

            //Assert
            Assert.False(result);

        }

        [Fact]
        private void DesertAnimalIsHiredInWinter_ReturnFalse_WhenTypeIsInvalid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals[1].Type = AnimalTypes.Sneeuw;

            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 01, 18);

            //Act
            var restult = animalSelectionValidator.DesertAnimalIsHiredInWinter(animals, dateTime);

            //Assert
            Assert.False(restult);

        }

        [Fact]
        private void DesertAnimalIsHiredInWinter_ReturnTrue_WhenDataIsValid()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            animals[1].Type = AnimalTypes.Woestijn;

            var animalSelectionValidator = new AnimalSelectionValidator();

            DateTime dateTime = new DateTime(2020, 1, 1);

            //Act
            var result = animalSelectionValidator.DesertAnimalIsHiredInWinter(animals, dateTime);

            //Assert
            Assert.True(result);

        }

        [Fact]
        private void IsAnimalAlreadyBooked_ThrowsNullReferenceException_WhenAnimalsAreNull()
        {
            //Arrange
            List<BookingAnimal> bookingAnimals = null;
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act
            Assert.Throws<NullReferenceException>(() => animalSelectionValidator.IsAnimalAlreadyBooked(bookingAnimals, 1));

            //Assert
        }

        [Fact]
        private void IsAnimalAlreadyBooked_ReturnsFalse_WhenIdIsInvalid()
        {
            //Arrange
            List<BookingAnimal> bookingAnimals = GetBookingAnimals();
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act
            var result = animalSelectionValidator.IsAnimalAlreadyBooked(bookingAnimals, -1);
            
            //Assert
            Assert.False(result);

        }

        [Fact]
        private void IsAnimalAlreadyBooked_ReturnsTrue_WhenDataIsValid()
        {
            //Arrange
            List<BookingAnimal> bookingAnimals = GetBookingAnimals();
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act
            var result = animalSelectionValidator.IsAnimalAlreadyBooked(bookingAnimals, 1);

            //Assert
            Assert.True(result);

        }


        [Fact]
        private void IsAnAnimalSelected_ThrowsNullReferenceException_WhenAnimalsAreNull()
        {
            //Arrange
            List<Animal> animals = null;
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act && Assert
            Assert.Throws<NullReferenceException>(() => animalSelectionValidator.IsAnAnimalSelected(animals));

        }

        [Fact]
        private void IsAnAnimalSelected_ReturnsFalse_WhenAnimalsAreZero()
        {
            //Arrange
            List<Animal> animals = new List<Animal>();
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act 
            var result = animalSelectionValidator.IsAnAnimalSelected(animals);
            
            //Assert
            Assert.False(result);
        }

        [Fact]
        private void IsAnAnimalSelected_ReturnsTrue_WhenAnimalsAreAboveZero()
        {
            //Arrange
            List<Animal> animals = GetAnimals();
            var animalSelectionValidator = new AnimalSelectionValidator();

            //Act 
            var result = animalSelectionValidator.IsAnAnimalSelected(animals);

            //Assert
            Assert.True(result);
        }

        private List<BookingAnimal> GetBookingAnimals()
        {
            List<BookingAnimal> bookingAnimals = new List<BookingAnimal>()
            {
                new BookingAnimal()
                {
                    AnimalId = 1,
                    Animal = GetAnimals()[0],
                    BookingId = 1,
                    Booking = new Booking()

                },
                new BookingAnimal()
                {
                    AnimalId = 2,
                    Animal = GetAnimals()[1],
                    BookingId = 2,
                    Booking = new Booking()

                },
                new BookingAnimal()
                {
                    AnimalId = 3,
                    Animal = GetAnimals()[2],
                    BookingId = 3,
                    Booking = new Booking()

                }
            };

            return bookingAnimals;
        }

        private List<Animal> GetAnimals()
        {
            List<Animal> animals = new List<Animal>()
            {
                new Animal()
                {
                    ID = 1,
                    Name = "Huseyin",
                    Type = AnimalTypes.Boerderij,
                    Price = 45.00,
                    PicturePath = "/images/animals/aap.png"
                },
                new Animal()
                {
                    ID = 2,
                    Name = "Ijsbeer",
                    Type = AnimalTypes.Woestijn,
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