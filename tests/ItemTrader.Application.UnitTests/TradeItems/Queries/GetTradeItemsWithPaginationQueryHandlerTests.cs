using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Application.TradeItems.Queries;
using ItemTrader.Application.TradeItems.Queries.Handlers;
using ItemTrader.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.TradeItems.Queries
{
    public class GetTradeItemsWithPaginationQueryHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldReturnAllTradeItemsWithEmptyQuery()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);
            _mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<TradeItem, TradeItemDto>(); }));

            var getTradeItemsWithPaginationQuery = new GetTradeItemsWithPaginationQuery();
            var handler = new GetTradeItemsWithPaginationQueryHandler(_context.Object, _mapper.Object);

            var tradeItems = await handler.Handle(getTradeItemsWithPaginationQuery, new CancellationToken());

            Assert.AreEqual(tradeItems.Items.Count, _tradeItems.Object.Count());
        }

        [Test]
        public async Task HandleShouldReturnOnlyTheRequestedTradeItemsWithName()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);
            _mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<TradeItem, TradeItemDto>(); }));

            var getTradeItemsWithPaginationQuery = new GetTradeItemsWithPaginationQuery{Name = "kırbaç" };
            var handler = new GetTradeItemsWithPaginationQueryHandler(_context.Object, _mapper.Object);

            var tradeItems = await handler.Handle(getTradeItemsWithPaginationQuery, new CancellationToken());

            Assert.AreEqual(tradeItems.Items.Count, _tradeItems.Object.Count(ti => ti.Name == "kırbaç"));
        }

        [Test]
        public async Task HandleShouldReturnOnlyTheRequestedTradeItemsWithOwnerId()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);
            _mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg => { cfg.CreateMap<TradeItem, TradeItemDto>(); }));

            var getTradeItemsWithPaginationQuery = new GetTradeItemsWithPaginationQuery { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb" };
            var handler = new GetTradeItemsWithPaginationQueryHandler(_context.Object, _mapper.Object);

            var tradeItems = await handler.Handle(getTradeItemsWithPaginationQuery, new CancellationToken());

            Assert.AreEqual(tradeItems.Items.Count, _tradeItems.Object.Count(ti => ti.OwnerId == "3813d77b-e04e-4a12-a036-fab1de9d16fb"));
        }
    }
}
