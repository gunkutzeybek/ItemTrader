using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Application.TradeItems.Queries;
using ItemTrader.Application.TradeItems.Queries.Handlers;
using ItemTrader.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.TradeItems.Queries
{
    public class GetSingleTradeItemQueryHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldReturnTheRequestedTradeItemWithOwnerIdAndItemId()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);
            _mapper.Setup(m => m.Map<TradeItemDto>(It.IsAny<TradeItem>())).Returns((TradeItem ti) => new TradeItemDto {Id = ti.Id});


            var query = new GetSingleTradeItemQuery {OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", TradeItemId = 1};
            var handler = new GetSingleTradeItemQueryHandler(_context.Object, _mapper.Object);

            var tradeItem = await handler.Handle(query, new CancellationToken());

            Assert.AreEqual(tradeItem.Id, query.TradeItemId);
        }

        [Test]
        public void HandleShouldThrowNotFoundExceptionIfTradeItemDoesNotExists()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            var query = new GetSingleTradeItemQuery { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", TradeItemId = 19 };
            var handler = new GetSingleTradeItemQueryHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(query, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Trade item couldn't be found.");
        }
    }
}
