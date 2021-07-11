using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using MediatR;

namespace ItemTrader.Application.Proposals.Queries
{
    public class GetProposalQuery : IRequest<ProposalDto>, IHasOwner
    {
        public string OwnerId { get; set; }
        public int ProposalId { get; set; }
    }
}
