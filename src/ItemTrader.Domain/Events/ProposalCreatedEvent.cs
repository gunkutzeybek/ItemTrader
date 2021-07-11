using ItemTrader.Domain.Common;
using ItemTrader.Domain.Entities;

namespace ItemTrader.Domain.Events
{
    public class ProposalCreatedEvent : DomainEvent
    {
        public Proposal Proposal { get; }

        public ProposalCreatedEvent(Proposal proposal)
        {
            Proposal = proposal;
        }
    }
}
