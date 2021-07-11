using ItemTrader.Domain.Common;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;

namespace ItemTrader.Domain.Events
{
    public class ProposalStatusUpdatedEvent : DomainEvent
    {
        public ProposalStatus OldStatus { get; }
        public ProposalStatus NewStatus { get; }
        public Proposal Proposal { get; }

        public ProposalStatusUpdatedEvent(ProposalStatus oldStatus, ProposalStatus newStatus, Proposal proposal)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
            Proposal = proposal;
        }
    }
}
