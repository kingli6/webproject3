using AutoMapper;
using College_API.Models;
using College_API.ViewModels;
using College_API.ViewModels.Category;

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

            CreateMap<PostCourseViewModel, Course>();
            CreateMap<Course, CourseViewModel>()
            .ForMember(dest => dest.CourseId, options => options.MapFrom(src => src.Id));

            CreateMap<PostCategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.Id));
        }
    }
}