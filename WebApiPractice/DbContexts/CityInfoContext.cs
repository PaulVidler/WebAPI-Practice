using Microsoft.EntityFrameworkCore;
using WebApiPractice.Entities;

namespace WebApiPractice.DbContexts
{
    public class CityInfoContext : DbContext
    {
        // packages installed
        // Microsoft.EntityFrameworkCore.SqlServer
        // Microsoft.EntityFrameworkCore.Tools - for console commands like add-migration etc
        // Microsoft.EntityFrameworkCore.Design


        // using this method means you'll have to set up the connection string in the program.cs file
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            :base(options) {
        }

        public DbSet<City> Cities { get; set; } = null!; // null forgiving operator to avoid null reference errors
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        // you can use this way to override the default table names that EF will use and to define the connection string, you can also do it in the constructor as above
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=;Trusted_Connection=True;");
        //}



    }
}
