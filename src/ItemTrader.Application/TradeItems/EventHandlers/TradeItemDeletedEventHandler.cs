using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Models;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ItemTrader.Application.TradeItems.EventHandlers
{
    public class TradeItemDeletedEventHandler : INotificationHandler<DomainEventNotification<TradeItemDeletedEvent>>
    {
        private readonly ILogger<TradeItemDeletedEventHandler> _logger;

        public TradeItemDeletedEventHandler(ILogger<TradeItemDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<TradeItemDeletedEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Trade item with {notification.DomainEvent.Item.Id} is deleted.");

            return Task.CompletedTask;
        }
    }
}
