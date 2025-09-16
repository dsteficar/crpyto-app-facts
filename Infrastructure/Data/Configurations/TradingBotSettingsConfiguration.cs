using Domain.Entity.Trading.Bots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TradingBotConfigurationConfiguration : IEntityTypeConfiguration<TradingBotSettings>
    {
        public void Configure(EntityTypeBuilder<TradingBotSettings> builder)
        {
            builder.HasKey(tbc => tbc.Id);

            builder.HasOne(tbc => tbc.TradingBotTask)
                   .WithOne(tb => tb.Settings)
                   .HasForeignKey<TradingBotSettings>(tbc => tbc.TradingBotTaskId);

            builder.HasIndex(us => us.TradingBotTaskId)
                .IsUnique();

            builder.Property(tbc => tbc.BaseAsset)
                   .HasMaxLength(10); 

            builder.Property(tbc => tbc.QuoteAsset)
                   .HasMaxLength(10); 

            builder.Property(tbc => tbc.SlotSize)
                   .HasPrecision(20, 8);

            builder.Property(tbc => tbc.StartingChannelPrice)
                   .HasPrecision(20, 8); 

            builder.Property(tbc => tbc.EndingChannelPrice)
                   .HasPrecision(20, 8);

            builder.Property(tbc => tbc.StopLossPrice)
                   .HasPrecision(20, 8);

            builder.Property(tbc => tbc.TriggerPrice)
                   .HasPrecision(20, 8);

            builder.Property(tbc => tbc.QuantityPerSlot)
                   .HasPrecision(20, 8);

            builder.Property(tbc => tbc.OrdersCount)
                   .HasDefaultValue(0);

            builder.Property(tbc => tbc.BotStatus)
                   .HasConversion<byte>() // Store as TINYINT
                   .IsRequired();

            builder.Property(tbc => tbc.MarketType)
                   .HasConversion<byte>()
                   .IsRequired();

            builder.Property(tbc => tbc.MarketDirection)
                   .HasConversion<byte>()
                   .IsRequired();

            builder.Property(tbc => tbc.ChannelStructureType)
                   .HasConversion<byte>()
                   .IsRequired();

            builder.Property(tbc => tbc.ChannelInfinityType)
                   .HasConversion<byte>()
                   .IsRequired();

            builder.Property(tbc => tbc.SlotType)
                   .HasConversion<byte>()
                   .IsRequired();

            builder.Property(tbc => tbc.QuantityType)
                   .HasConversion<byte>()
                   .IsRequired();
        }
    }
}
