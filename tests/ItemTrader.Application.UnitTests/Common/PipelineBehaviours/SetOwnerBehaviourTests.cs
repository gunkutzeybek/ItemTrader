using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Common.PipelineBehaviours;
using ItemTrader.Application.Proposals.Commands;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Application.TradeItems.Queries;
using MediatR;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.Common.PipelineBehaviours
{
    public class SetOwnerBehaviourTests
    {
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<RequestHandlerDelegate<ProposalDto>> _next;

        public SetOwnerBehaviourTests()
        {
            _currentUserService = new Mock<ICurrentUserService>();
            _next = new Mock<RequestHandlerDelegate<ProposalDto>>();
        }

        [Test]
        public async Task ShouldSetOwnerIdOnRequestForHasOwnerCommand()
        {
            _currentUserService.Setup(c => c.UserId).Returns("3813d77b-e04e-4a12-a036-fab1de9d16fb");

            var setOwnerBehaviour = new SetOwnerBehaviour<CreateProposalCommand, ProposalDto>(_currentUserService.Object);

            var command = new CreateProposalCommand();
            await setOwnerBehaviour.Handle(command, new CancellationToken(), _next.Object);

            Assert.AreEqual(command.OwnerId, "3813d77b-e04e-4a12-a036-fab1de9d16fb");
        }

        [Test]
        public async Task ShouldNotSetOwnerIdRequestForNotHasOwnerQueries()
        {
            _currentUserService.Setup(c => c.UserId).Returns("3813d77b-e04e-4a12-a036-fab1de9d16fb");

            var setOwnerBehaviour = new SetOwnerBehaviour<GetTradeItemsWithPaginationQuery, ProposalDto>(_currentUserService.Object);

            var query = new GetTradeItemsWithPaginationQuery();
            await setOwnerBehaviour.Handle(query, new CancellationToken(), _next.Object);

            Assert.IsNull(query.OwnerId);
        }
    }
}
