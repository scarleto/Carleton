using AutoMapper;

namespace Carleton.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.UserDto>();
            CreateMap<Models.UserForCreationDto, Entities.User>();
            CreateMap<Models.UserForUpdateDto, Entities.User>();
            CreateMap<Entities.User, Models.UserForUpdateDto>();
        }
    }
}
