using CityInfo.API;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice.Models;
using WebApiPractice.Models.UpdateDtos;

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

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
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

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestDto pointOfInterest)
        {
            // the code below is not needed due to it being made available in the [ApiController] attribute

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}


            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // demo purposes only, will be imporved later in the course
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointofinterestid = finalPointOfInterest.Id
            }, finalPointOfInterest);
        }
        
        
        
        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            
            // find the city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            // check if the city exists
            if (city == null)
            {
                return NotFound();
            }
            // find the point of interest
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            // check if the point of interest exists
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            // update the point of interest
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;
            // return the point of interest
            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        // JsonPatchDocument is a package that will auto check the changed values and only update those where necessary
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            // find the city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            // check if the city exists
            if (city == null)
            {
                return NotFound();
            }
            // find the point of interest
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            // check if the point of interest exists
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            // create a new instance of the PointOfInterestForUpdateDto
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };
            // apply the patch to the pointOfInterestToPatch
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            // check if the model state is valid - comes from the ModelState aergument in the line above
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // check if the pointOfInterestToPatch is valid
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            // update the point of interest
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            // return the point of interest
            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            // find the city
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            // check if the city exists
            if (city == null)
            {
                return NotFound();
            }
            // find the point of interest
            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            // check if the point of interest exists
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            // delete the point of interest
            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            // return the point of interest
            return NoContent();
        }
    }
}
