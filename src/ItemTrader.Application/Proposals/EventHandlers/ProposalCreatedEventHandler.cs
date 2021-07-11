using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Common.Models;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ItemTrader.Application.Proposals.EventHandlers
{
    public class ProposalCreatedEventHandler : INotificationHandler<DomainEventNotification<ProposalCreatedEvent>>
    {
        private readonly ILogger<ProposalCreatedEventHandler> _logger;

        public ProposalCreatedEventHandler(ILogger<ProposalCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProposalCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var proposal = notification.DomainEvent.Proposal;
            _logger.LogWarning($"Proposal created with id {proposal.Id} for owner {proposal.OwnerId}.");

            return Task.CompletedTask;  
        }
    }
}
