using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using ItemTrader.Domain.Events;
using MediatR;

namespace ItemTrader.Application.TradeItems.Commands.Handlers
{
    public class CreateTradeItemCommandHandler : IRequestHandler<CreateTradeItemCommand, TradeItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTradeItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TradeItemDto> Handle(CreateTradeItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TradeItem
            {
                OwnerId = request.OwnerId,
                Name = request.Name,
                Status = TradeItemStatus.Listed
            };

            entity.DomainEvents.Add(new TradeItemCreatedEvent(entity));

            _context.TradeItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TradeItemDto>(entity);
        }
    }
}
