using AutoMapper;
using ecommerce.Core.Dtos;
using ecommerce.Core.Models;

namespace ecommerce.Core.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add your mapping configurations here
        // For example:
        // CreateMap<SourceModel, DestinationModel>();
        CreateMap<ApplicationUser, AuthResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
    }
}
