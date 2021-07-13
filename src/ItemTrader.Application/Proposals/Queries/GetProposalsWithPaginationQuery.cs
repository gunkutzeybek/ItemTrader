using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Common.Models;
using ItemTrader.Application.Proposals.Dto;
using MediatR;

namespace ItemTrader.Application.Proposals.Queries
{
    public class GetProposalsWithPaginationQuery : IRequest<PaginatedList<ProposalDto>>, IHasOwner
    {
        public string OwnerId { get; set; }
        public int? Status { get; set; }
        public int OfferedItemId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
