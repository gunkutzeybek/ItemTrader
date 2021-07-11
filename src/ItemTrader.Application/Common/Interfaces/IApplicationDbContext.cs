using ItemTrader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ItemTrader.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TradeItem> TradeItems { get; set; }
        DbSet<Proposal> Proposals { get; set; }

        DbSet<Trader> Traders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
