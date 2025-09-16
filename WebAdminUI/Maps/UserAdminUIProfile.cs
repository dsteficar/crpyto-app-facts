using AutoMapper;
using Domain.Entity.Authentication;
using WebAdminUI.Models.Users;

namespace WebAdminUI.Maps
{
    public class UserAdminUIProfile : Profile
    {
        public UserAdminUIProfile()
        {
            CreateEntityToModel();
            CreateModelToEntity();
        }

        private void CreateEntityToModel()
        {
            CreateMap<ApplicationUser, UserDataGridAdminModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => src.SecurityStamp))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src =>
                    src.LockoutEnd.HasValue
                        ? src.LockoutEnd.Value.ToOffset(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)).DateTime
                        : (DateTime?)null))
                            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => src.TwoFactorEnabled));
        }

        private void CreateModelToEntity()
        {
            CreateMap<UserDataGridAdminModel, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => src.SecurityStamp))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src =>
                    src.LockoutEnd.HasValue
                        ? new DateTimeOffset(src.LockoutEnd.Value, TimeZoneInfo.Local.GetUtcOffset(src.LockoutEnd.Value))
                        : (DateTimeOffset?)null))
                            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => src.TwoFactorEnabled));
        }
    }
}

