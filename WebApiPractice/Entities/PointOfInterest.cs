using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiPractice.Entities
{
    public class PointOfInterest
    {

        [Key] // this will ensure that the Id property is the primary key in the database when using Entity Framework
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // this will ensure that the Id property is auto-generated when using Entity Framework
        public int Id { get; set; } // the name Id is a convention that EF will pick up on and use as the primary key
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        // foreign key
        [ForeignKey("CityId")] // this will explicitly tell EF that this is the foreign key for the prop below
        public City? City { get; set; } // this is a navigation property and will define the relationship between City and POI, and will be the foreign key
        public int CityId { get; set; } // this is not required, but seems like a good convention to follow

        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}
