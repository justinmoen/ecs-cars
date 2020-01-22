using System;
using System.Collections.Generic;
using Xunit;
using CarsApi.Models;
using CarsApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CarsApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetAllCars_ShouldReturnCorrectCount()
        {
            //arrange
            var testCars = GetTestCars();
            var controller = new CarsController();

            //act
            var result = controller.GetAllCars() as List<Car>;

            //assert
            Assert.True(result.Count == 3);
        }

        [Fact]
        public void GetCarsById_ShouldReturnCorrectCar()
        {
            //arrange
            var testCars = GetTestCars();
            var controller = new CarsController();

            //act
            var result = controller.GetCar(1) as OkObjectResult;
            var car = result.Value as Car;

            //assert
            Assert.True(car.Id == 1 &&
                car.Make == "Oldsmobile" &&
                car.Model == "Cutlass Sierra" &&
                car.Colour == "Dark Purple"
            );
        }     

        private List<Car> GetTestCars()
        {
            return new List<Car>()
            {
                new Car{Id = 0, Make = "Mercury", Model = "Sable", Colour = "Silver"},
                new Car{Id = 1, Make = "Oldsmobile", Model = "Cutlass Sierra", Colour = "Dark Purple"},
                new Car{Id = 2, Make = "Ford", Model = "Fusion", Colour = "Black"}
            };

        }
    }
}
