using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using MediatR;

namespace ItemTrader.Application.Proposals.Commands
{
    public class UpdateProposalStatusCommand : IRequest<ProposalDto>, IHasOwner
    {
        public string OwnerId { get; set; }
        public int ProposalId { get; set; }
        public int Status { get; set; }
    }
}
