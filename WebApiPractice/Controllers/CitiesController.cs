using AutoMapper;
using CityInfo.API;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice.Models;
using WebApiPractice.Services;

namespace WebApiPractice.Controllers
{

    // this was just a class that was created from a generic "add->Class", then the attributes were added

    [ApiController]
    [Route("api/cities")] // this can be defined at a controller level or at a method level
    // [Route("api/[controller]")] - this is how you define a route at the controller level using the name of the controller by default
    public class CitiesController : ControllerBase
    {
        
        // constructor
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new System.ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }
        
        
        
        // this is used instead of setting up routing. Definitely looks like a better way to do it and much easier to read
        // [HttpGet("api/cities")] - this is how you define a route at the method level
        [HttpGet] // this is how you define a the Get request because the route is defined at controller level
        // public JsonResult GetCities()
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities() // this is the same as above but it is using the ActionResult class
        {
            //return Ok(_citiesDataStore.Cities);
            // this is no "NotFound() here because the CitiesDataStore.Current.Cities
            // is a list and will return an empty list if there is nothing in it and that is a valid response

            // new implementation using repoistory
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));

        }

        [HttpGet("{id}")] // this is showing the use of a parameter in the route
        //public JsonResult GetCity(int id) // JSONResult implements IActionResult and is an ActionResult
        // in this one, we're using an IActionResult as there could actually be 2 different return types based on the logic below
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            //// pulls data from the CitiesDataStore. Current is a property of this class
            //var citiesToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);

            //if (citiesToReturn == null)
            //{
            //    return NotFound(); // this will return a 404 status code
            //}

            //return Ok(citiesToReturn); // this will return a 200 status code

            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound(); // this will return a 404 status code
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));

        }
    }
}
