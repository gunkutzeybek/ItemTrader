using ItemTrader.Domain.Common;
using ItemTrader.Domain.Entities;

namespace ItemTrader.Domain.Events
{
    public class TradeItemCreatedEvent : DomainEvent
    {
        public TradeItem Item { get; }

        public TradeItemCreatedEvent(TradeItem item)
        {
            Item = item;
        }
    }
}
