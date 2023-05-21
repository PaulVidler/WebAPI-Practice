using AutoMapper;

namespace WebApiPractice.Profiles
{
    // this is a class that is used to setup automapper to map one object to another
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.CreationDtos.PointOfInterestForCreationDto, Entities.PointOfInterest>();
        }
    }
}