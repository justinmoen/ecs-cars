using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CarsApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CarsApi.Repositories
{
    public class CarsRepository : IRepository<Car>
    {
        private const string datamuseUri = "https://api.datamuse.com/words?sl={0}&max=6";
        private readonly CarContext _context;
        public CarsRepository(CarContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            var cars = await _context.Car.ToListAsync();
            foreach (var car in cars)
            {
                car.Homonyms = await GetHomonyms(car.Model);
            }
            return cars;
        }

        public Car Create(Car entity)
        {
            throw new System.NotImplementedException();
        }

        public Car Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Car GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Car Update(int id)
        {
            throw new System.NotImplementedException();
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

            return homonyms.TrimEnd(new char[] { ' ', ',' });
        }
    }
}