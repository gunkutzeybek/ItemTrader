using ItemTrader.Application.Common.Models;
using ItemTrader.Application.TradeItems.Dto;
using MediatR;

namespace ItemTrader.Application.TradeItems.Queries
{
    public class GetTradeItemsWithPaginationQuery : IRequest<PaginatedList<TradeItemDto>>
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
