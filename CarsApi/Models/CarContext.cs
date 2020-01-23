using Microsoft.EntityFrameworkCore;

namespace CarsApi.Models
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options)
            : base(options)
        {
        }

        public DbSet<Car> CarItems { get; set; }
    }
}