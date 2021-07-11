using ItemTrader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemTrader.Infrastructure.Persistence.Configurations
{
    public class TraderConfiguration : IEntityTypeConfiguration<Trader>
    {
        public void Configure(EntityTypeBuilder<Trader> builder)
        {
            builder.ToTable("AspNetUsers", t => t.ExcludeFromMigrations());

            builder.HasAlternateKey(t => t.Id);

            builder.Property(b => b.UserName)
                .HasField("_userName");

            builder.Property(b => b.Email)
                .HasField("_email");
        }
    }
}
