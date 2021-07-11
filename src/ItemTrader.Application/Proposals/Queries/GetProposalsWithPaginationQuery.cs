using ItemTrader.Application.Common.Models;
using ItemTrader.Application.Proposals.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItemTrader.Application.Proposals.Queries
{
    public class GetProposalsWithPaginationQuery : IRequest<PaginatedList<ProposalDto>>
    {
        public string OwnerId { get; set; }
        public int? Status { get; set; }
        public string ProposedToId { get; set; }
        public int OfferedItemId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
