using ItemTrader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemTrader.Infrastructure.Persistence.Configurations
{
    public class TradeItemConfiguration : IEntityTypeConfiguration<TradeItem>
    {
        public void Configure(EntityTypeBuilder<TradeItem> builder)
        {
            builder.Property(t => t.OwnerId)
                .HasMaxLength(450)
                .IsRequired();

            builder.HasOne(t => t.Owner)
                .WithMany(t => t.TradeItems)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Ignore(t => t.DomainEvents);
        }
    }
}
