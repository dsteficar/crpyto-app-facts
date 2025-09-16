using Domain.Entity.Trading.Bots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TradingBotTaskConfiguration : IEntityTypeConfiguration<TradingBotTask>
    {
        public void Configure(EntityTypeBuilder<TradingBotTask> builder)
        {
            builder.HasKey(tbc => tbc.Id);

            builder.HasOne(tbt => tbt.User)
               .WithMany(tbc => tbc.TradeBotTasks)
               .HasForeignKey(us => us.UserId);

            builder.Property(tbc => tbc.Name)
                    .HasMaxLength(20); 

            builder.Property(tbc => tbc.IsEnabled)
                    .HasDefaultValue(false);
        }
    }
}
