using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarsApi.Models;


namespace CarsApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarsContext _carsContext;
        private static readonly List<Car> cars = new List<Car>()
        {
            new Car{Id = 0, Make = "Mercury", Model = "Sable", Colour = "Silver"},
            new Car{Id = 1, Make = "Oldsmobile", Model = "Cutlass Sierra", Colour = "Dark Purple"},
            new Car{Id = 2, Make = "Ford", Model = "Fusion", Colour = "Black"}
        };

        //private readonly ILogger<CarsController> _logger;

        public CarsController(CarsContext carsContext /*ILogger<CarsController> logger*/)
        {
            _carsContext = carsContext;
            //_logger = logger;
        }

        [HttpGet]
        public IEnumerable<Car> GetAllCars()
        {
            return cars;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCar(int id)
        {
            var car = cars.FirstOrDefault(car => car.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _carsContext.CarItems.Add(car);
            await _carsContext.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }




        // [HttpGet]
        // public ActionResult<List<Car>> GetAllCars()
        // {
        //     if (cars == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(cars);
        //     //return Task.FromResult(cars);
        // }
    }
}
