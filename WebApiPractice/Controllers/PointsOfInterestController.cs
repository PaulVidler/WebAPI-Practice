using CityInfo.API;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice.Models;

namespace WebApiPractice.Controllers
{

    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);

        }

        [HttpGet("{pointofinterestid}")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointofinterestid)
        {
            // find the city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            // check if the city exists
            if (city == null)
            {
                return NotFound();
            }

            // find the point of interest
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestid);
            // check if the point of interest exists
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            // return the point of interest
            return Ok(pointOfInterest);
        }
    }
}
