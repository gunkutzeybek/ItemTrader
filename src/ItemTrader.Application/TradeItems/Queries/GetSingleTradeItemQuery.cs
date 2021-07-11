using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Dto;
using MediatR;

namespace ItemTrader.Application.TradeItems.Queries
{
    public class GetSingleTradeItemQuery : IRequest<TradeItemDto>, IHasOwner
    {
        public string OwnerId { get; set; }
        public int TradeItemId { get; set; }
    }
}
