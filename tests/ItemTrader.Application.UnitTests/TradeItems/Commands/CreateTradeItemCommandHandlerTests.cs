using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.TradeItems.Commands;
using ItemTrader.Application.TradeItems.Commands.Handlers;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Moq;

namespace ItemTrader.Application.UnitTests.TradeItems.Commands
{
    public class CreateTradeItemCommandHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldCallsSaveChangesWithValidParameters()
        {
            var mockSet = new Mock<DbSet<TradeItem>>();
            _context.Setup(c => c.TradeItems).Returns(mockSet.Object);
            _mapper.Setup(m => m.Map<TradeItemDto>(It.IsAny<TradeItem>())).Returns(new TradeItemDto());

            var createTradeItemCommand = new CreateTradeItemCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                Name = "trade item 1"
            };

            var handler = new CreateTradeItemCommandHandler(_context.Object, _mapper.Object);
            await handler.Handle(createTradeItemCommand, new CancellationToken());

            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task HandleShouldCallContextAddWithValidParameters()
        {
            var mockSet = new Mock<DbSet<TradeItem>>();
            _context.Setup(c => c.TradeItems).Returns(mockSet.Object);
            _mapper.Setup(m => m.Map<TradeItemDto>(It.IsAny<TradeItem>())).Returns(new TradeItemDto());

            var createTradeItemCommand = new CreateTradeItemCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                Name = "trade item 1"
            };

            var handler = new CreateTradeItemCommandHandler(_context.Object, _mapper.Object);
            await handler.Handle(createTradeItemCommand, new CancellationToken());

            mockSet.Verify(m => 
                m.Add(It.Is<TradeItem>(
                    ti => ti.Name == createTradeItemCommand.Name && 
                          ti.OwnerId == createTradeItemCommand.OwnerId &&
                          ti.Status == TradeItemStatus.Listed)));
        }
    }
}
