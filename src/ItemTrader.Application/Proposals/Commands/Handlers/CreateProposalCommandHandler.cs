using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemTrader.Application.Proposals.Dto;

namespace ItemTrader.Application.Proposals.Commands.Handlers
{
    public class CreateProposalCommandHandler : IRequestHandler<CreateProposalCommand, ProposalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProposalCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProposalDto> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
        {
            var offeredItem = await GetTradeItemAsync(request.OfferedItemId);
            var requestedItem = await GetTradeItemAsync(request.RequestedItemId);

            ValidateProposalItems(offeredItem, requestedItem, request.OwnerId);

            var entity = new Proposal
            {
                OwnerId = request.OwnerId,
                OfferedItemId = request.OfferedItemId,
                RequestedItemId = request.RequestedItemId
            };

            entity.ProposedToId = requestedItem.OwnerId;
            offeredItem.Status = TradeItemStatus.InProposal;

            _context.Proposals.Add(entity);

            entity.DomainEvents.Add(new ProposalCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProposalDto>(entity);
        }

        private void ValidateProposalItems(TradeItem offeredItem, TradeItem requestedItem, string requestOwnerId)
        {
            ValidateOfferedItem(offeredItem, requestOwnerId);
            ValidateRequestedItem(requestedItem, requestOwnerId);
        }

        private void ValidateOfferedItem(TradeItem offeredItem, string requestOwnerId)
        {
            if (offeredItem == null)
            {
                throw new ProposalItemException("Offered item does not exist.");
            }

            if (offeredItem.Status == TradeItemStatus.InProposal)
            {
                throw new ProposalItemException("The trade item is already offered in another proposal. Please cancel it first.");
            }

            if (offeredItem.Status == TradeItemStatus.Traded)
            {
                throw new ProposalItemException("Offered trade item is not listed for trading.");
            }

            if (offeredItem.OwnerId != requestOwnerId)
            {
                throw new ProposalItemException("trade item", offeredItem.Id);
            }
        }

        private void ValidateRequestedItem(TradeItem requestedItem, string requestOwnerId)
        {
            if (requestedItem.OwnerId == requestOwnerId)
            {
                throw new ProposalItemException("Requested trade item belongs to user.");
            }

            if (requestedItem.Status == TradeItemStatus.Traded)
            {
                throw new ProposalItemException("Requested trade item is not listed for trading.");
            }
        }

        private async Task<TradeItem> GetTradeItemAsync(int tradeItemId)
        {
            return await _context.TradeItems
                .SingleOrDefaultAsync(ti => ti.Id == tradeItemId);
        }
    }
}
