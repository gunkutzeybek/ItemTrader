using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItemTrader.Application.Proposals.Commands.Handlers
{
    public class UpdateProposalStatusCommandHandler : IRequestHandler<UpdateProposalStatusCommand, ProposalDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProposalStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProposalDto> Handle(UpdateProposalStatusCommand request, CancellationToken cancellationToken)
        {
            var proposal = await _context.Proposals
                .Include(p => p.OfferedItem)
                .Include(p => p.RequestedItem)
                .SingleOrDefaultAsync(p => 
                    p.Id == request.ProposalId && 
                    (p.OwnerId == request.OwnerId || p.ProposedToId == request.OwnerId), 
                    cancellationToken);

            ValidateProposalUpdate(request, proposal);

            var requestedStatus = (ProposalStatus) request.Status;

            proposal.DomainEvents.Add(new ProposalStatusUpdatedEvent(proposal.Status, requestedStatus, proposal));

            proposal.Status = requestedStatus;

            UpdateTradeItems(proposal);

            await CancelAllProposalsHavingTheseItemsAsync(proposal);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProposalDto>(proposal);
        }

        private void UpdateTradeItems(Proposal proposal)
        {
            switch (proposal.Status)
            {
                case ProposalStatus.Accepted:
                    proposal.OfferedItem.Status = TradeItemStatus.Traded;
                    proposal.RequestedItem.Status = TradeItemStatus.Traded;
                    SwapOwners(proposal.OfferedItem, proposal.RequestedItem);
                    break;
                case ProposalStatus.Cancelled:
                case ProposalStatus.Rejected:
                    proposal.OfferedItem.Status = TradeItemStatus.Listed;
                    break;
            }
        }

        private void SwapOwners(TradeItem offeredItem, TradeItem requestedItem)
        {
            var tempOwner = offeredItem.OwnerId;
            offeredItem.OwnerId = requestedItem.OwnerId;
            requestedItem.OwnerId = tempOwner;
        }

        private async Task CancelAllProposalsHavingTheseItemsAsync(Proposal proposal)
        {
            if (proposal.Status == ProposalStatus.Accepted)
            {
                var toCancel = _context.Proposals
                    .Where(p => p.Id != proposal.Id && p.Status == ProposalStatus.Active &&
                                (p.RequestedItemId == proposal.RequestedItemId ||
                                 p.OfferedItemId == proposal.RequestedItemId ||
                                 p.RequestedItemId == proposal.OfferedItemId ||
                                 p.OfferedItemId == proposal.OfferedItemId))
                    .AsQueryable();

                await toCancel.ForEachAsync((p) => { p.Status = ProposalStatus.Cancelled; });
            }
        }

        private void ValidateProposalUpdate(UpdateProposalStatusCommand command, Proposal proposal)
        {
            if (proposal == null)
            {
                throw new NotFoundException("Proposal couldn't be found.");
            }

            if (proposal.OwnerId == command.OwnerId && command.Status != (int) ProposalStatus.Cancelled)
            {
                throw new ProposalItemException("Proposal owner is only allowed to cancel the proposal.");
            }

            if (proposal.ProposedToId == command.OwnerId && command.Status != (int) ProposalStatus.Accepted && command.Status != (int)ProposalStatus.Rejected)
            {
                throw new ProposalItemException("Reciepent of the proposal only allowed to accept or reject the proposal.");
            }

            if (proposal.Status == ProposalStatus.Cancelled && command.Status == (int)ProposalStatus.Active)
            {
                throw new ProposalItemException("Cancelled proposal cannot be activated again. Please create a new proposal.");
            }

            if (proposal.Status == ProposalStatus.Cancelled && command.Status == (int)ProposalStatus.Accepted)
            {
                throw new ProposalItemException("Cancelled proposal cannot be accepted. Please create a new proposal.");
            }

            if (proposal.Status == ProposalStatus.Cancelled && command.Status == (int)ProposalStatus.Rejected)
            {
                throw new ProposalItemException("Cancelled proposal cannot be rejected.");
            }
        }
    }
}
