using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPractice.Entities
{
    public class City
    {
        [Key] // this will ensure that the Id property is the primary key in the database when using Entity Framework
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // this will ensure that the Id property is auto-generated when using Entity Framework
        public int Id { get; set; } // the name Id or even CityId is a convention that EF will pick up on and use as the primary key
        
        [Required]
        [MaxLength(50)] // exclicitly directing the max length and that it is not nullable
        public string Name { get; set; } // cannot have a null value. It could be initialised as string.empty, but see the constructor below as a better way of doing this
        
        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; } // good idea to initialise collections in the constructor
        = new List<PointOfInterest>();

        public City(string name) // this ensures the name value is initialised on creation of the object
        {
            Name = name;
        }
    }
}
