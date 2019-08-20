using AutoMapper;
using payApp.API.Dtos;
using payApp.API.Models;

namespace payApp.API.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForUpdatedDto, User>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForLoginDto, User>();
            CreateMap<WishForAddDto, Wish>();
        }
    }
}