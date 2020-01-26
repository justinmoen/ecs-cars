using System;
using System.Collections.Generic;
using Xunit;
using CarsApi.Models;
using CarsApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.EntityFrameworkCore;
using CarsApi.Repositories;
using System.Threading.Tasks;

namespace CarsApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void GetAllCars_ShouldReturnCorrectCount()
        {
            //arrange
            var mock = new Mock<IRepository<Car>>();
            mock.Setup(m => m.GetAll()).ReturnsAsync(GetTestCars());
            var controller = new CarsController(null, mock.Object);

            //act
            var cars = await controller.GetCar() as OkObjectResult;

            //assert
            var result = cars.Value as List<Car>;
            Assert.Equal(result.Count, 3);
        }

        [Fact]
        public void GetCarsById_ShouldReturnCorrectCar()
        {
            //arrange
            // var testCars = GetTestCars();
            // var controller = new CarsController();

            // //act
            // var result = controller.GetCar(1) as OkObjectResult;
            // var car = result.Value as Car;

            // //assert
            // Assert.True(car.Id == 1 &&
            //     car.Make == "Oldsmobile" &&
            //     car.Model == "Cutlass Sierra" &&
            //     car.Colour == "Dark Purple"
            // );
        }

        [Fact] 
        public void PostCar_CreatesACar()
        {
            
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
