using Application.DTOs.Account.Responses;
using AutoMapper;
using Domain.ValueObjects.Tokens;

namespace Application.Maps
{
    public class UserTokenProfile : Profile
    {
        public UserTokenProfile()
        {
            CreateResultObjectToDtoMap();
        }

        private void CreateResultObjectToDtoMap() // Mapping from ApplicationUser to UserDTO
        {
            CreateMap<UserTokenResult, LoginResponseDTO>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));

            CreateMap<UserTokenResult, RegisterResponseDTO>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));

            CreateMap<UserTokenResult, GetRefreshTokenResponseDTO>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken));
        }
    }
}
