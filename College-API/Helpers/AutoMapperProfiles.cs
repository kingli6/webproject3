using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using College_API.Models;
using College_API.ViewModels;

namespace College_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        // Map från -> till
        public AutoMapperProfiles()
        {
            CreateMap<PostUserViewModel, User>();
            CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, options => options.MapFrom(src => string.Concat(src.FirstName, " ", src.LastName)));
        }
    }
}