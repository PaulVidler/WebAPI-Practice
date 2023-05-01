using CityInfo.API;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice.Models;

namespace WebApiPractice.Controllers
{

    // this was just a class that was created from a generic "add->Class", then the attributes were added

    [ApiController]
    [Route("api/cities")] // this can be defined at a controller level or at a method level
    // [Route("api/[controller]")] - this is how you define a route at the controller level using the name of the controller by default
    public class CitiesController : ControllerBase
    {
        
        // constructor
        private readonly ICitiesDataStore _citiesDataStore;

        public CitiesController(ICitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore;
        }
        
        
        
        // this is used instead of setting up routing. Definitely looks like a better way to do it and much easier to read
        // [HttpGet("api/cities")] - this is how you define a route at the method level
        [HttpGet] // this is how you define a the Get request because the route is defined at controller level
        // public JsonResult GetCities()
        public ActionResult<IEnumerable<CityDto>> GetCities() // this is the same as above but it is using the ActionResult class
        {
            return Ok(_citiesDataStore.Cities);
            // this is no "NotFound() here because the CitiesDataStore.Current.Cities
            // is a list and will return an empty list if there is nothing in it and that is a valid response
        }

        [HttpGet("{id}")] // this is showing the use of a parameter in the route
        //public JsonResult GetCity(int id) // JSONResult implements IActionResult and is an ActionResult
        public ActionResult<CityDto> GetCity(int id)
        {
            // pulls data from the CitiesDataStore. Current is a property of this class
            var citiesToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            if(citiesToReturn == null)
            {
                return NotFound(); // this will return a 404 status code
            }

            return Ok(citiesToReturn); // this will return a 200 status code
        }
    }
}
