using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Common.Mappings;
using ItemTrader.Application.Common.Models;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItemTrader.Application.TradeItems.Queries.Handlers
{
    public class GetTradeItemsWithPaginationQueryHandler : IRequestHandler<GetTradeItemsWithPaginationQuery, PaginatedList<TradeItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTradeItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TradeItemDto>> Handle(GetTradeItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.TradeItems
                .AsNoTracking()
                .Where(x => 
                    (string.IsNullOrWhiteSpace(request.Name) || x.Name == request.Name) 
                    && (string.IsNullOrWhiteSpace(request.OwnerId) || x.OwnerId == request.OwnerId))
                .Include(t => t.Owner)
                .OrderByDescending(t => t.Created)
                .ProjectTo<TradeItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
