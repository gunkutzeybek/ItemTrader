using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Models;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ItemTrader.Application.Proposals.EventHandlers
{
    public class ProposalStatusUpdatedEventHandler : INotificationHandler<DomainEventNotification<ProposalStatusUpdatedEvent>>
    {
        private readonly ILogger<ProposalStatusUpdatedEventHandler> _logger;

        public ProposalStatusUpdatedEventHandler(ILogger<ProposalStatusUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ProposalStatusUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Proposal status is updated from {notification.DomainEvent.OldStatus} to {notification.DomainEvent.NewStatus} of proposal {notification.DomainEvent.Proposal.Id}.");

            return Task.CompletedTask;
        }
    }
}
