using ItemTrader.Application.Common.Interfaces;
using MediatR;

namespace ItemTrader.Application.TradeItems.Commands
{
    public class DeleteTradeItemCommand : IRequest, IHasOwner
    {
        public string OwnerId { get; set; }
        public int TradeItemId { get; set; }
    }
}
