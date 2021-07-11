using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Proposals.Commands;
using ItemTrader.Application.Proposals.Commands.Handlers;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Domain.Entities;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Interfaces;

namespace ItemTrader.Application.UnitTests.Proposals.Commands.Handlers
{
    public class CreateProposalCommandHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldCallsSaveChangesWithValidParameters()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 3,
                RequestedItemId = 1
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);
            await handler.Handle(createProposalCommand, new CancellationToken());

            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithNonExistingOfferItem()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 10, //this item does not exist.
                RequestedItemId = 1
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(createProposalCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Offered item does not exist.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithAnInProposalTradeItem()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 5, //this item has InProposal status.
                RequestedItemId = 1
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(createProposalCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "The trade item is already offered in another proposal. Please cancel it first.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithAnotherTradersItem()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 2, //this item is owned by another trader.
                RequestedItemId = 1
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(createProposalCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "The trade item resource with id 2 does not belong to user.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithARequestToOwnTradeItem()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 4, //this item belongs to same user with the requested item.
                RequestedItemId = 3 //this item belongs to same user with the offered item.
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);

            var ex =Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(createProposalCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Requested trade item belongs to user.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithARequestedItemNotAvailableForTrading()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 4, 
                RequestedItemId = 6 //this item status is traded. So it is not available for trading.
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(createProposalCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Requested trade item is not listed for trading.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithAnOfferedItemNotAvailableForTrading()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var createProposalCommand = new CreateProposalCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                OfferedItemId = 7,
                RequestedItemId = 2 //this item status is traded. So it is not available for trading.
            };

            var handler = new CreateProposalCommandHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(createProposalCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Offered trade item is not listed for trading.");
        }
    }
}
