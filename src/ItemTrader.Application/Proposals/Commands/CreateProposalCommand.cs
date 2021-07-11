using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using MediatR;

namespace ItemTrader.Application.Proposals.Commands
{
    public class CreateProposalCommand : IRequest<ProposalDto>, IHasOwner
    {
        public string OwnerId { get; set; }
        public int OfferedItemId { get; set; }
        public int RequestedItemId { get; set; }
    }
}
