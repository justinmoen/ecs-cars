using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarsApi.Models;

    public class CarContext : DbContext
    {
        public CarContext (DbContextOptions<CarContext> options)
            : base(options)
        {
        }

        public DbSet<CarsApi.Models.Car> Car { get; set; }
    }
