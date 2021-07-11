using ItemTrader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemTrader.Infrastructure.Persistence.Configurations
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.Property(t => t.OwnerId)
                .HasMaxLength(450)
                .IsRequired();

            builder.HasOne(p => p.Owner)
                .WithMany(t => t.Proposals)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.ProposedToId)
                .HasMaxLength(450)
                .IsRequired();

            builder.HasOne(p => p.ProposedTo)
                .WithMany(t => t.RecievedProposals)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.OfferedItem)
                .WithMany(t => t.OfferedProposals)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.RequestedItem)
                .WithMany(t => t.RequestedProposals)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.Status)
                .IsConcurrencyToken()
                .IsRequired();

            builder.Ignore(p => p.DomainEvents);
        }
    }
}
