using ItemTrader.Domain.Common;
using MediatR;

namespace ItemTrader.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
    {
        public TDomainEvent DomainEvent { get; }
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
