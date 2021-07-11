using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Commands;
using ItemTrader.Application.TradeItems.Commands.Handlers;
using ItemTrader.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.TradeItems.Commands
{
    public class DeleteTradeItemCommandHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldCallsSaveChangesWithValidParameters()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            var deleteTradeItemCommand = new DeleteTradeItemCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                TradeItemId = 1
            };
            var handler = new DeleteTradeItemCommandHandler(_context.Object);
            await handler.Handle(deleteTradeItemCommand, new CancellationToken());

            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task HandleShouldCallDbSetRemoveOnceWithValidParameters()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            var deleteTradeItemCommand = new DeleteTradeItemCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                TradeItemId = 1
            };
            var handler = new DeleteTradeItemCommandHandler(_context.Object);
            await handler.Handle(deleteTradeItemCommand, new CancellationToken());

            _tradeItems.Verify(
                ti => ti.Remove(It.Is<TradeItem>(
                    ti => ti.Id == deleteTradeItemCommand.TradeItemId &&
                          ti.OwnerId == deleteTradeItemCommand.OwnerId)));
        }

        [Test]
        public void HandleShouldThrowNotFoundExceptionIfTradeItemDoesNotExist()
        {
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);
            
            var deleteTradeItemCommand = new DeleteTradeItemCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                TradeItemId = 17
            };
            var handler = new DeleteTradeItemCommandHandler(_context.Object);
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(deleteTradeItemCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Trade item couldn't be found.");
        }
    }
}
