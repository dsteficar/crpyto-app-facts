using Application.DTOs.Base;
using Application.DTOs.ParallelChannel.Requests;
using Application.DTOs.ParallelChannel.Responses;
using AutoMapper;
using Domain.Entity.Trading.Graphs;

namespace Application.Maps
{
    public class ParallelChannelProfile : Profile
    {
        public ParallelChannelProfile()
        {

            CreateRequestDtoToEntityMaps();
            CreateEntityToResponseDtoMap();
        }

        private void CreateRequestDtoToEntityMaps()
        {
            CreateMap<AddParallelChannelRequestDTO, ParallelChannel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<UpdateParallelChannelRequestDTO, ParallelChannel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3));

            CreateMap<ParallelChannelDTO, ParallelChannel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
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

        private void CreateEntityToResponseDtoMap()
        {
            //CreateMap<ParallelChannel, ParallelChannelResponseDTO>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            //    .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
            //    .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
            //    .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
            //    .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
            //    .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
            //    .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3))
            //    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<ParallelChannel, GetParallelChannelDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());


            CreateMap<ParallelChannel, ParallelChannelDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<ParallelChannel, AddParallelChannelResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<ParallelChannel, UpdateParallelChannelResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3));

            CreateMap<ParallelChannel, GetUserParallelChannelResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3));

            CreateMap<ParallelChannel, GetParallelChannelResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Price1, opt => opt.MapFrom(src => src.Price1))
                .ForMember(dest => dest.Price2, opt => opt.MapFrom(src => src.Price2))
                .ForMember(dest => dest.Price3, opt => opt.MapFrom(src => src.Price3))
                .ForMember(dest => dest.Timestamp1, opt => opt.MapFrom(src => src.Timestamp1))
                .ForMember(dest => dest.Timestamp2, opt => opt.MapFrom(src => src.Timestamp2))
                .ForMember(dest => dest.Timestamp3, opt => opt.MapFrom(src => src.Timestamp3));
        }
    }
}
