using Microsoft.AspNetCore.Mvc;

namespace WebApiPractice.Controllers
{

    // this was just a class that was created from a generic "add->Class", then the attributes were added

    [ApiController]
    [Route("api/cities")] // this can be defined at a controller level or at a method level
    // [Route("api/[controller]")] - this is how you define a route at the controller level using the name of the controller by default
    public class CitiesController : ControllerBase
    {
        // this is used instead of setting up routing. Definitely looks like a better way to do it and much easier to read
        // [HttpGet("api/cities")] - this is how you define a route at the method level
        [HttpGet] // this is how you define a the Get request because the route is defined at controller level
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                    new { id = 1, Name = "New York City" },
                    new { id = 2, Name = "Antwerp" }
                });
        }
    }
}
