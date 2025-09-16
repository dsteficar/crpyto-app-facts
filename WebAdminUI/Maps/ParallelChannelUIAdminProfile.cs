using AutoMapper;
using Domain.Entity.Trading.Graphs;
using WebAdminUI.Models.ParallelChannels;

namespace WebAdminUI.Maps
{
    public class ParallelChannelUIAdminProfile : Profile
    {
        public ParallelChannelUIAdminProfile()
        {
            CreateEntityToModel();
            CreateModelToEntity();
        }

        private void CreateEntityToModel()
        {
            CreateMap<ParallelChannel, ParallelChannelDataGridAdminModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3));
        }

        private void CreateModelToEntity()
        {
            CreateMap<ParallelChannelDataGridAdminModel, ParallelChannel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }

    }
}
