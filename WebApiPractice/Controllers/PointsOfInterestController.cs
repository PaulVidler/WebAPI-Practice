using AutoMapper;
using CityInfo.API;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiPractice.Models;
using WebApiPractice.Models.UpdateDtos;
using WebApiPractice.Services;

namespace WebApiPractice.Controllers
{

    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]

    public class PointsOfInterestController : ControllerBase
    {
        
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            //try
            //{
            //    // throw new Exception("exception sample"); - this is just testiong
                
            //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            //    if (city == null)
            //    {
            //        _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest");
            //        return NotFound();
            //    }

            //    return Ok(city.PointsOfInterest);
            //}
            //catch (Exception ex)
            //{
            //    // because this is an exception, it will be logged as critical
            //    _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", ex);

            //    // instead of just throwing the exception, we are returning a status code and a message. It is important not
            //    // to return the exception message because it could contain sensitive information. Keep it simple
            //    return StatusCode(500, "A problem happened while handling your request"); 
            //}

            
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing points of interest");
                return NotFound();
            }

            var pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
            
            

        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public async Task <ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointofinterestid)
        {
            //// find the city
            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //// check if the city exists
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// find the point of interest
            //var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestid);
            //// check if the point of interest exists
            //if (pointOfInterest == null)
            //{
            //    return NotFound();
            //}
            //// return the point of interest
            //return Ok(pointOfInterest);

            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            
            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestid);

            if(pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestDto pointOfInterest)
        {
            // the code below is not needed due to it being made available in the [ApiController] attribute

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}


            //var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //// demo purposes only, will be imporved later in the course
            //var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            //var finalPointOfInterest = new PointOfInterestDto()
            //{
            //    Id = ++maxPointOfInterestId,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            //city.PointsOfInterest.Add(finalPointOfInterest);

            //return CreatedAtRoute("GetPointOfInterest", new
            //{
            //    cityId = cityId,
            //    pointofinterestid = finalPointOfInterest.Id
            //}, finalPointOfInterest);

            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();

            var createdPointOfInterestToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                { cityId, pointofinterestid = createdPointOfInterestToReturn.Id },
                createdPointOfInterestToReturn);

        }



        //[HttpPut("{pointofinterestid}")]
        //public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        //{

        //    // find the city
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    // check if the city exists
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    // find the point of interest
        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    // check if the point of interest exists
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //    // update the point of interest
        //    pointOfInterestFromStore.Name = pointOfInterest.Name;
        //    pointOfInterestFromStore.Description = pointOfInterest.Description;
        //    // return the point of interest
        //    return NoContent();
        //}

        //[HttpPatch("{pointofinterestid}")]
        //// JsonPatchDocument is a package that will auto check the changed values and only update those where necessary
        //public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{
        //    // find the city
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    // check if the city exists
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    // find the point of interest
        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    // check if the point of interest exists
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //    // create a new instance of the PointOfInterestForUpdateDto
        //    var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
        //    {
        //        Name = pointOfInterestFromStore.Name,
        //        Description = pointOfInterestFromStore.Description
        //    };
        //    // apply the patch to the pointOfInterestToPatch
        //    patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
        //    // check if the model state is valid - comes from the ModelState aergument in the line above
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    // check if the pointOfInterestToPatch is valid
        //    if (!TryValidateModel(pointOfInterestToPatch))
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    // update the point of interest
        //    pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        //    pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        //    // return the point of interest
        //    return NoContent();
        //}

        //[HttpDelete("{pointofinterestid}")]
        //public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        //{
        //    // find the city
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    // check if the city exists
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }
        //    // find the point of interest
        //    var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    // check if the point of interest exists
        //    if (pointOfInterestFromStore == null)
        //    {
        //        return NotFound();
        //    }
        //    // delete the point of interest
        //    city.PointsOfInterest.Remove(pointOfInterestFromStore);

        //    // send an email to the user to confirm the deletion
        //    _mailService.Send("Point of interest deleted.", 
        //        $"Point of interest {pointOfInterestFromStore.Name}" +
        //        $" with id {pointOfInterestFromStore.Id} was deleted.");

        //    return NoContent();
        //}
    }
}
