using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Dto;
using MediatR;

namespace ItemTrader.Application.TradeItems.Commands
{
    public class CreateTradeItemCommand : IRequest<TradeItemDto>, IHasOwner
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
    }
}
