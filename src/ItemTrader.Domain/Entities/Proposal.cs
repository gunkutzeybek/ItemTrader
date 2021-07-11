using System.Collections.Generic;
using ItemTrader.Domain.Common;
using ItemTrader.Domain.Common.Interfaces;
using ItemTrader.Domain.Enums;
using ItemTrader.Domain.Events;

namespace ItemTrader.Domain.Entities
{
    public class Proposal : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public Trader Owner { get; set; }
        public string ProposedToId { get; set; }
        public Trader ProposedTo { get; set; }
        public ProposalStatus Status { get; set; }

        public int OfferedItemId { get; set; }
        public TradeItem OfferedItem { get; set; }

        public int RequestedItemId { get; set; }
        public TradeItem RequestedItem { get; set; }

        public IList<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
