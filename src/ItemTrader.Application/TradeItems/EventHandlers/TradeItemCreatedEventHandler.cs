using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Models;
using ItemTrader.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ItemTrader.Application.TradeItems.EventHandlers
{
    public class TradeItemCreatedEventHandler : INotificationHandler<DomainEventNotification<TradeItemCreatedEvent>>
    {
        private readonly ILogger<TradeItemCreatedEventHandler> _logger;

        public TradeItemCreatedEventHandler(ILogger<TradeItemCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<TradeItemCreatedEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Trade item with id {notification.DomainEvent.Item.Id} is created.");

            return Task.CompletedTask;
        }
    }
}
