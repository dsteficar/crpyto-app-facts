using Application.DTOs.Admin.Account.Responses;
using Application.DTOs.Response.User;
using AutoMapper;
using Domain.Entity.Authentication;

namespace Application.Maps
{
    public class UserAdminProfile : Profile
    {
        public UserAdminProfile()
        {
            CreateDtoToEntityMap();
            CreateEntityToDtoMap();
        }

        private void CreateDtoToEntityMap()
        {
        }

        private void CreateEntityToDtoMap()
        {
            CreateMap<ApplicationUser, LoginAdminResponseDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}