using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ItemTrader.Application.TradeItems.Commands.Handlers
{
    public class DeleteTradeItemCommandHandler : IRequestHandler<DeleteTradeItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTradeItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTradeItemCommand request, CancellationToken cancellationToken)
        {
            var tradeItem = await _context.TradeItems
                .SingleOrDefaultAsync(ti => ti.OwnerId == request.OwnerId && ti.Id == request.TradeItemId,
                    cancellationToken);

            if (tradeItem == null)
            {
                throw new NotFoundException("Trade item couldn't be found.");
            }

            tradeItem.DomainEvents.Add(new TradeItemDeletedEvent(tradeItem));

            _context.TradeItems.Remove(tradeItem);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
