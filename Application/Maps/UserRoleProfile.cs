using Application.DTOs.Account.Responses;
using AutoMapper;
using Domain.ResultObjects.Roles;

namespace Application.Maps
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateResultObjectToDtoMap();
        }

        private void CreateResultObjectToDtoMap()
        {
            CreateMap<UserRoleResult, GetUserRoleResponseDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
        }
    }
}
