using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Application.Proposals.Queries;
using ItemTrader.Application.Proposals.Queries.Handlers;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.Proposals.Queries.Handlers
{
    public class GetProposalQueryHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldReturnProposalWithValidParameters()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>()))
                .Returns((Proposal p) =>
                    new ProposalDto
                    {
                        OwnerId = p.OwnerId, Id = p.Id, ProposedToId = p.ProposedToId, Status = (int) p.Status
                    });

            var query = new GetProposalQuery
            {
                OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4",
                ProposalId = 1
            };
            var handler = new GetProposalQueryHandler(_context.Object, _mapper.Object);

            var result = await handler.Handle(query, new CancellationToken());

            Assert.AreEqual(result.OwnerId, query.OwnerId);
            Assert.AreEqual(result.Id, query.ProposalId);
        }

        [Test]
        public void HandleShouldThrowNotFoundExceptionForAnotherUsersProposal()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _mapper.Setup(m => m.Map<ProposalDto>(It.IsAny<Proposal>()))
                .Returns((Proposal p) =>
                    new ProposalDto
                    {
                        OwnerId = p.OwnerId,
                        Id = p.Id,
                        ProposedToId = p.ProposedToId,
                        Status = (int)p.Status
                    });

            var query = new GetProposalQuery
            {
                OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb",
                ProposalId = 1
            };
            var handler = new GetProposalQueryHandler(_context.Object, _mapper.Object);

            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(query, new CancellationToken());
            });

            Assert.AreEqual(ex.Message, "Resource couldn't be found.");
        }
    }
}
