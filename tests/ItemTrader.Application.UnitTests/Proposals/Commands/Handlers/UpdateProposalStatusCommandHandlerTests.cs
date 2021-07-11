using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Commands;
using ItemTrader.Application.Proposals.Commands.Handlers;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.Proposals.Commands.Handlers
{
    public class UpdateProposalStatusCommandHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleCallsSaveChangesWithValidParameters()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var updateProposalStatusCommand = new UpdateProposalStatusCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                ProposalId = 2,
                Status = (int)ProposalStatus.Accepted
            };

            var handler = new UpdateProposalStatusCommandHandler(_context.Object, _mapper.Object);
            await handler.Handle(updateProposalStatusCommand, new CancellationToken());

            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public void HandleShouldThrowNotFoundExceptionIfTheProposalCouldnNotBeFound()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var updateProposalStatusCommand = new UpdateProposalStatusCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                ProposalId = 4,
                Status = (int)ProposalStatus.Accepted
            };

            var handler = new UpdateProposalStatusCommandHandler(_context.Object, _mapper.Object);
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(updateProposalStatusCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Proposal couldn't be found.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithUserTryingToAcceptOwnProposal()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var updateProposalStatusCommand = new UpdateProposalStatusCommand
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                ProposalId = 2,
                Status = (int)ProposalStatus.Accepted
            };

            var handler = new UpdateProposalStatusCommandHandler(_context.Object, _mapper.Object);
            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(updateProposalStatusCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Proposal owner is only allowed to cancel the proposal.");
        }

        [Test]
        public void HandleShouldThrowProposalItemExceptionWithUserCancelsARecievedProposal()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _context.Setup(c => c.TradeItems).Returns(_tradeItems.Object);

            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>())).Returns(new ProposalDto());

            var updateProposalStatusCommand = new UpdateProposalStatusCommand
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                ProposalId = 2,
                Status = (int)ProposalStatus.Cancelled
            };

            var handler = new UpdateProposalStatusCommandHandler(_context.Object, _mapper.Object);
            var ex = Assert.ThrowsAsync<ProposalItemException>(async () =>
            {
                await handler.Handle(updateProposalStatusCommand, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Reciepent of the proposal only allowed to accept or reject the proposal.");
        }
    }
}
