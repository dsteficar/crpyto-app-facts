using Application.DTOs.TradeBots.Requests;
using Application.DTOs.TradeBots.Responses;
using AutoMapper;
using WebApplicationUI.Models.Trading.Bots;

namespace WebApplicationUI.Maps
{
    public class TradingBotAppProfile : Profile
    {
        public TradingBotAppProfile()
        {
            CreateDtoToModelMap();
            CreateModelToDtoMap();
        }

        public void CreateDtoToModelMap()
        {
            CreateMap<GetUserTradingBotSettingsResponseDTO, TradingBotEditFormModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled))
                .ForMember(dest => dest.BotStatus, opt => opt.MapFrom(src => src.BotStatus))
                .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.MarketType))
                .ForMember(dest => dest.MarketDirection, opt => opt.MapFrom(src => src.MarketDirection))
                .ForMember(dest => dest.ChannelStructureType, opt => opt.MapFrom(src => src.ChannelStructureType))
                .ForMember(dest => dest.ChannelInfinityType, opt => opt.MapFrom(src => src.ChannelInfinityType))
                .ForMember(dest => dest.BaseAsset, opt => opt.MapFrom(src => src.BaseAsset))
                .ForMember(dest => dest.QuoteAsset, opt => opt.MapFrom(src => src.QuoteAsset))
                .ForMember(dest => dest.PricePrecision, opt => opt.MapFrom(src => src.PricePrecision))
                .ForMember(dest => dest.QuantityPrecision, opt => opt.MapFrom(src => src.QuantityPrecision))
                .ForMember(dest => dest.SlotType, opt => opt.MapFrom(src => src.SlotType))
                .ForMember(dest => dest.SlotSize, opt => opt.MapFrom(src => src.SlotSize))
                .ForMember(dest => dest.StartingChannelPrice, opt => opt.MapFrom(src => src.StartingChannelPrice))
                .ForMember(dest => dest.EndingChannelPrice, opt => opt.MapFrom(src => src.EndingChannelPrice))
                .ForMember(dest => dest.StopLossPrice, opt => opt.MapFrom(src => src.StopLossPrice))
                .ForMember(dest => dest.TriggerPrice, opt => opt.MapFrom(src => src.TriggerPrice))
                .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => src.QuantityType))
                .ForMember(dest => dest.QuantityPerSlot, opt => opt.MapFrom(src => src.QuantityPerSlot))
                .ForMember(dest => dest.NumberOfSlots, opt => opt.MapFrom(src => src.NumberOfSlots));
        }

        public void CreateModelToDtoMap()
        {
            // Map TradeBotEditFormModel -> AddUserTradeBotRequestDTO
            CreateMap<TradingBotEditFormModel, AddUserTradeBotRequestDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled))
                .ForMember(dest => dest.BotStatus, opt => opt.MapFrom(src => src.BotStatus))
                .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.MarketType))
                .ForMember(dest => dest.MarketDirection, opt => opt.MapFrom(src => src.MarketDirection))
                .ForMember(dest => dest.ChannelStructureType, opt => opt.MapFrom(src => src.ChannelStructureType))
                .ForMember(dest => dest.ChannelInfinityType, opt => opt.MapFrom(src => src.ChannelInfinityType))
                .ForMember(dest => dest.BaseAsset, opt => opt.MapFrom(src => src.BaseAsset))
                .ForMember(dest => dest.QuoteAsset, opt => opt.MapFrom(src => src.QuoteAsset))
                .ForMember(dest => dest.PricePrecision, opt => opt.MapFrom(src => src.PricePrecision))
                .ForMember(dest => dest.QuantityPrecision, opt => opt.MapFrom(src => src.QuantityPrecision))
                .ForMember(dest => dest.SlotType, opt => opt.MapFrom(src => src.SlotType))
                .ForMember(dest => dest.SlotSize, opt => opt.MapFrom(src => src.SlotSize))
                .ForMember(dest => dest.StartingChannelPrice, opt => opt.MapFrom(src => src.StartingChannelPrice))
                .ForMember(dest => dest.EndingChannelPrice, opt => opt.MapFrom(src => src.EndingChannelPrice))
                .ForMember(dest => dest.StopLossPrice, opt => opt.MapFrom(src => src.StopLossPrice))
                .ForMember(dest => dest.TriggerPrice, opt => opt.MapFrom(src => src.TriggerPrice))
                .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => src.QuantityType))
                .ForMember(dest => dest.QuantityPerSlot, opt => opt.MapFrom(src => src.QuantityPerSlot))
                .ForMember(dest => dest.NumberOfSlots, opt => opt.MapFrom(src => src.NumberOfSlots));

            // Map TradeBotEditFormModel -> UpdateUserTradeBotRequestDTO
            CreateMap<TradingBotEditFormModel, UpdateUserTradeBotRequestDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled))
                .ForMember(dest => dest.BotStatus, opt => opt.MapFrom(src => src.BotStatus))
                .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.MarketType))
                .ForMember(dest => dest.MarketDirection, opt => opt.MapFrom(src => src.MarketDirection))
                .ForMember(dest => dest.ChannelStructureType, opt => opt.MapFrom(src => src.ChannelStructureType))
                .ForMember(dest => dest.ChannelInfinityType, opt => opt.MapFrom(src => src.ChannelInfinityType))
                .ForMember(dest => dest.BaseAsset, opt => opt.MapFrom(src => src.BaseAsset))
                .ForMember(dest => dest.QuoteAsset, opt => opt.MapFrom(src => src.QuoteAsset))
                .ForMember(dest => dest.PricePrecision, opt => opt.MapFrom(src => src.PricePrecision))
                .ForMember(dest => dest.QuantityPrecision, opt => opt.MapFrom(src => src.QuantityPrecision))
                .ForMember(dest => dest.SlotType, opt => opt.MapFrom(src => src.SlotType))
                .ForMember(dest => dest.SlotSize, opt => opt.MapFrom(src => src.SlotSize))
                .ForMember(dest => dest.StartingChannelPrice, opt => opt.MapFrom(src => src.StartingChannelPrice))
                .ForMember(dest => dest.EndingChannelPrice, opt => opt.MapFrom(src => src.EndingChannelPrice))
                .ForMember(dest => dest.StopLossPrice, opt => opt.MapFrom(src => src.StopLossPrice))
                .ForMember(dest => dest.TriggerPrice, opt => opt.MapFrom(src => src.TriggerPrice))
                .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => src.QuantityType))
                .ForMember(dest => dest.QuantityPerSlot, opt => opt.MapFrom(src => src.QuantityPerSlot))
                .ForMember(dest => dest.NumberOfSlots, opt => opt.MapFrom(src => src.NumberOfSlots));
        }
    }
}
