using AutoMapper;

namespace WebApiPractice.Profiles
{
    // this is a class that is used to setup automapper to map one object to another
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}
