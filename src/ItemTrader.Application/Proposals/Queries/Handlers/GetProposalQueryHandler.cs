using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItemTrader.Application.Proposals.Queries.Handlers
{
    public class GetProposalQueryHandler : IRequestHandler<GetProposalQuery, ProposalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProposalQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProposalDto> Handle(GetProposalQuery request, CancellationToken cancellationToken)
        {
            var proposal = await _context.Proposals
                .AsNoTracking()
                .ProjectTo<ProposalDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.OwnerId == request.OwnerId && p.Id == request.ProposalId, cancellationToken);

            if (proposal == null)
            {
                throw new NotFoundException("Resource couldn't be found.");
            }

            return proposal;
        }
    }
}
