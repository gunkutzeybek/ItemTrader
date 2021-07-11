using ItemTrader.Domain.Common;
using ItemTrader.Domain.Entities;

namespace ItemTrader.Domain.Events
{
    public class TradeItemDeletedEvent : DomainEvent
    {
        public TradeItem Item { get; }

        public TradeItemDeletedEvent(TradeItem item)
        {
            Item = item;
        }
    }
}
