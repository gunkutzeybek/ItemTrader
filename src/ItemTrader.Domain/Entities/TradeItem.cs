using System.Collections.Generic;
using ItemTrader.Domain.Common;
using ItemTrader.Domain.Common.Interfaces;
using ItemTrader.Domain.Enums;

namespace ItemTrader.Domain.Entities
{
    public class TradeItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public Trader Owner { get; set; }
        public string Name { get; set; }
        public TradeItemStatus Status { get; set; }

        public List<Proposal> OfferedProposals { get; set; }
        public List<Proposal> RequestedProposals { get; set; }

        public IList<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
