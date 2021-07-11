using ItemTrader.Application.TradeItems.Dto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ItemTrader.Application.TradeItems.Queries.Handlers
{
    public class GetSingleTradeItemQueryHandler : IRequestHandler<GetSingleTradeItemQuery, TradeItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleTradeItemQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TradeItemDto> Handle(GetSingleTradeItemQuery request, CancellationToken cancellationToken)
        {
            var tradeItem = await _context.TradeItems
                .SingleOrDefaultAsync(ti => ti.OwnerId == request.OwnerId && ti.Id == request.TradeItemId, cancellationToken);

            if (tradeItem == null)
            {
                throw new NotFoundException("Trade item couldn't be found.");
            }

            return _mapper.Map<TradeItemDto>(tradeItem);
        }
    }
}
