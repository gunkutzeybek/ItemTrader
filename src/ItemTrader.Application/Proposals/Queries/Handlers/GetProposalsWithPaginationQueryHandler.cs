using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Common.Mappings;
using ItemTrader.Application.Common.Models;
using ItemTrader.Application.Proposals.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItemTrader.Application.Proposals.Queries.Handlers
{
    public class GetProposalsWithPaginationQueryHandler : IRequestHandler<GetProposalsWithPaginationQuery, PaginatedList<ProposalDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProposalsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<PaginatedList<ProposalDto>> Handle(GetProposalsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return _context.Proposals
                .AsNoTracking()
                .Where(p =>
                    (string.IsNullOrWhiteSpace(request.ProposedToId) || p.ProposedToId == request.ProposedToId) &&
                    (request.OfferedItemId == default(int) || p.OfferedItemId == request.OfferedItemId) &&
                    (request.Status == null || request.Status.Value == (int) p.Status) &&
                    p.OwnerId == request.OwnerId
                )
                .ProjectTo<ProposalDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

        }
    }
}
