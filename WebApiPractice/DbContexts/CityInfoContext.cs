using Microsoft.EntityFrameworkCore;
using WebApiPractice.Entities;
using WebApiPractice.Models;

namespace WebApiPractice.DbContexts
{
    public class CityInfoContext : DbContext
    {
        // packages installed
        // Microsoft.EntityFrameworkCore.SqlServer
        // Microsoft.EntityFrameworkCore.Tools - for console commands like add-migration etc
        // Microsoft.EntityFrameworkCore.Design

        // package Manager console commands to create migration: add-migration CityInfoInitialMigration
        // package Manager console commands to update database: update-database - this is the one that will actually create the database


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


        // this below is about seeding the database with some data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // I'd guess you could easily add Faker in here fairly easily and create a shit-tonne of objects
            
            modelBuilder.Entity<City>().HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The one with that big park.",
                },
                new City("Antwerp")
                {
                    Id = 2,
                    Description = "The one with the cathedral that was never really finished.",
                },
                new City("Paris")
                {
                    Id = 3,
                    Description = "The one with that big tower.",
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                     new PointOfInterest("Eiffel Tower") {
                         Id = 1,
                         CityId = 3,
                         Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
                     },
                      new PointOfInterest("The Louvre") {
                          Id = 2,
                          CityId = 3,
                          Description = "The world's largest museum."
                      },
                       new PointOfInterest("Central Park")
                       {
                           Id = 3,
                           CityId = 1,
                           Description = "Big Park in the USA"
                       },
                      new PointOfInterest("Empire State Building")
                      {
                          Id = 4,
                          CityId = 1,
                          Description = "TBig building in the USA"
                      },
                      new PointOfInterest("Cathedral of Our Lady")
                      {
                          Id = 5,
                          CityId = 2,
                          Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
                      },
                     new PointOfInterest("Antwerp Central Station")
                     {
                         Id = 6,
                         CityId = 2,
                         Description = "The the finest example of railway architecture in Belgium."
                     });
                

            base.OnModelCreating(modelBuilder);
        }



    }
}
