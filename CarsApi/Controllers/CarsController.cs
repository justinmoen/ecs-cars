using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsApi.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CarsApi.Repositories;

namespace CarsApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private const string datamuseUri = "https://api.datamuse.com/words?sl={0}&max=6";
        private readonly CarContext _context;
        private readonly IRepository<Car> _carsRepository;

        public CarsController(CarContext context, IRepository<Car> repository)
        {
            _context = context;
            _carsRepository = repository;
        }

        // GET: api/v1/Cars
        [HttpGet]
        public async Task<ActionResult> GetCar()
        {
            return Ok(await _carsRepository.GetAll()); 
        }

        // GET: api/v1/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
            
            car.Homonyms = await GetHomonyms(car.Model);

            return car;
        }

        // PUT: api/v1/Cars/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/v1/Cars
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/v1/Cars/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }

        private async Task<string> GetHomonyms(string model)
        {
            string homonyms = string.Empty;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(string.Format(datamuseUri, model));
            response.EnsureSuccessStatusCode(); //would want to add error handling here in case datamuse went down
            string content = await response.Content.ReadAsStringAsync();
            var objects = JArray.Parse(content);
            foreach (var root in objects)
            {
                var homonym = root["word"].ToString();
                if (model.Equals(homonym, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                homonyms += homonym + ", ";
            }

            return homonyms.TrimEnd(new char[]{' ', ','});
        }
    }
}
