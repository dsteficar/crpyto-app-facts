using Application.DTOs.TradeBots.Requests;
using Application.DTOs.TradeBots.Responses;
using AutoMapper;
using Domain.Entity.Trading.Bots;

namespace Application.Maps
{
    public class TradeBotProfile : Profile
    {
        public TradeBotProfile() {
            CreateEntityToDtoMap();
            CreateDtoToEntityMap();
        }

        public void CreateEntityToDtoMap()
        {
            CreateMap<TradingBotTask, GetUserTradingBotSettingsResponseDTO>()
                .ForMember(dest => dest.BotStatus, opt => opt.MapFrom(src => src.Settings.BotStatus))
                .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.Settings.MarketType))
                .ForMember(dest => dest.MarketDirection, opt => opt.MapFrom(src => src.Settings.MarketDirection))
                .ForMember(dest => dest.ChannelStructureType, opt => opt.MapFrom(src => src.Settings.ChannelStructureType))
                .ForMember(dest => dest.ChannelInfinityType, opt => opt.MapFrom(src => src.Settings.ChannelInfinityType))
                .ForMember(dest => dest.BaseAsset, opt => opt.MapFrom(src => src.Settings.BaseAsset))
                .ForMember(dest => dest.QuoteAsset, opt => opt.MapFrom(src => src.Settings.QuoteAsset))
                .ForMember(dest => dest.PricePrecision, opt => opt.MapFrom(src => src.Settings.PricePrecision))
                .ForMember(dest => dest.QuantityPrecision, opt => opt.MapFrom(src => src.Settings.QuantityPrecision))
                .ForMember(dest => dest.SlotType, opt => opt.MapFrom(src => src.Settings.SlotType))
                .ForMember(dest => dest.SlotSize, opt => opt.MapFrom(src => src.Settings.SlotSize))
                .ForMember(dest => dest.StartingChannelPrice, opt => opt.MapFrom(src => src.Settings.StartingChannelPrice))
                .ForMember(dest => dest.EndingChannelPrice, opt => opt.MapFrom(src => src.Settings.EndingChannelPrice))
                .ForMember(dest => dest.StopLossPrice, opt => opt.MapFrom(src => src.Settings.StopLossPrice))
                .ForMember(dest => dest.TriggerPrice, opt => opt.MapFrom(src => src.Settings.TriggerPrice))
                .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => src.Settings.QuantityType))
                .ForMember(dest => dest.QuantityPerSlot, opt => opt.MapFrom(src => src.Settings.QuantityPerSlot))
                .ForMember(dest => dest.NumberOfSlots, opt => opt.MapFrom(src => src.Settings.NumberOfSlots));

            CreateMap<TradingBotTask, AddUserTradeBotResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled))
                .ForMember(dest => dest.BotStatus, opt => opt.MapFrom(src => src.Settings.BotStatus))
                .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.Settings.MarketType))
                .ForMember(dest => dest.MarketDirection, opt => opt.MapFrom(src => src.Settings.MarketDirection))
                .ForMember(dest => dest.ChannelStructureType, opt => opt.MapFrom(src => src.Settings.ChannelStructureType))
                .ForMember(dest => dest.ChannelInfinityType, opt => opt.MapFrom(src => src.Settings.ChannelInfinityType))
                .ForMember(dest => dest.BaseAsset, opt => opt.MapFrom(src => src.Settings.BaseAsset))
                .ForMember(dest => dest.QuoteAsset, opt => opt.MapFrom(src => src.Settings.QuoteAsset))
                .ForMember(dest => dest.PricePrecision, opt => opt.MapFrom(src => src.Settings.PricePrecision))
                .ForMember(dest => dest.QuantityPrecision, opt => opt.MapFrom(src => src.Settings.QuantityPrecision))
                .ForMember(dest => dest.SlotType, opt => opt.MapFrom(src => src.Settings.SlotType))
                .ForMember(dest => dest.SlotSize, opt => opt.MapFrom(src => src.Settings.SlotSize))
                .ForMember(dest => dest.StartingChannelPrice, opt => opt.MapFrom(src => src.Settings.StartingChannelPrice))
                .ForMember(dest => dest.EndingChannelPrice, opt => opt.MapFrom(src => src.Settings.EndingChannelPrice))
                .ForMember(dest => dest.StopLossPrice, opt => opt.MapFrom(src => src.Settings.StopLossPrice))
                .ForMember(dest => dest.TriggerPrice, opt => opt.MapFrom(src => src.Settings.TriggerPrice))
                .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => src.Settings.QuantityType))
                .ForMember(dest => dest.QuantityPerSlot, opt => opt.MapFrom(src => src.Settings.QuantityPerSlot))
                .ForMember(dest => dest.NumberOfSlots, opt => opt.MapFrom(src => src.Settings.NumberOfSlots));

            CreateMap<TradingBotTask, UpdateUserTradeBotResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled))
                .ForMember(dest => dest.BotStatus, opt => opt.MapFrom(src => src.Settings.BotStatus))
                .ForMember(dest => dest.MarketType, opt => opt.MapFrom(src => src.Settings.MarketType))
                .ForMember(dest => dest.MarketDirection, opt => opt.MapFrom(src => src.Settings.MarketDirection))
                .ForMember(dest => dest.ChannelStructureType, opt => opt.MapFrom(src => src.Settings.ChannelStructureType))
                .ForMember(dest => dest.ChannelInfinityType, opt => opt.MapFrom(src => src.Settings.ChannelInfinityType))
                .ForMember(dest => dest.BaseAsset, opt => opt.MapFrom(src => src.Settings.BaseAsset))
                .ForMember(dest => dest.QuoteAsset, opt => opt.MapFrom(src => src.Settings.QuoteAsset))
                .ForMember(dest => dest.PricePrecision, opt => opt.MapFrom(src => src.Settings.PricePrecision))
                .ForMember(dest => dest.QuantityPrecision, opt => opt.MapFrom(src => src.Settings.QuantityPrecision))
                .ForMember(dest => dest.SlotType, opt => opt.MapFrom(src => src.Settings.SlotType))
                .ForMember(dest => dest.SlotSize, opt => opt.MapFrom(src => src.Settings.SlotSize))
                .ForMember(dest => dest.StartingChannelPrice, opt => opt.MapFrom(src => src.Settings.StartingChannelPrice))
                .ForMember(dest => dest.EndingChannelPrice, opt => opt.MapFrom(src => src.Settings.EndingChannelPrice))
                .ForMember(dest => dest.StopLossPrice, opt => opt.MapFrom(src => src.Settings.StopLossPrice))
                .ForMember(dest => dest.TriggerPrice, opt => opt.MapFrom(src => src.Settings.TriggerPrice))
                .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => src.Settings.QuantityType))
                .ForMember(dest => dest.QuantityPerSlot, opt => opt.MapFrom(src => src.Settings.QuantityPerSlot))
                .ForMember(dest => dest.NumberOfSlots, opt => opt.MapFrom(src => src.Settings.NumberOfSlots));

            CreateMap<TradingBotTask, GetUserTradeBotResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled));
        }

        public void CreateDtoToEntityMap()
        {
            // Map AddUserTradeBotRequest to TradeBotTask
            CreateMap<AddUserTradeBotRequestDTO, TradingBotTask>()
                .ForMember(dest => dest.Settings, opt => opt.Ignore()) // Ignore Configuration since it's a separate mapping
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled));

            // Map AddUserTradeBotRequest to TradeBotSettings
            CreateMap<AddUserTradeBotRequestDTO, TradingBotSettings>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let the database assign the ID
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

            // Map UpdateUserTradeBotRequestDTO to TradeBotTask
            CreateMap<UpdateUserTradeBotRequestDTO, TradingBotTask>()
                .ForMember(dest => dest.Settings, opt => opt.Ignore()) // Ignore Configuration since it's a separate mapping
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled));

            // Map UpdateUserTradeBotRequestDTO to TradeBotSettings
            CreateMap<UpdateUserTradeBotRequestDTO, TradingBotSettings>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Let the database handle the ID
                .ForMember(dest => dest.TradingBotTaskId, opt => opt.MapFrom(src => src.Id))
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
