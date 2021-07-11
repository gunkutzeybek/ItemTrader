using System.Threading.Tasks;
using ItemTrader.Domain.Common;

namespace ItemTrader.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
