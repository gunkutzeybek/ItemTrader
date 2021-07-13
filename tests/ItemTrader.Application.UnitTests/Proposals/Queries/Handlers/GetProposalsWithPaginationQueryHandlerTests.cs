using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Application.Proposals.Queries;
using ItemTrader.Application.Proposals.Queries.Handlers;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Application.TradeItems.Queries;
using ItemTrader.Application.TradeItems.Queries.Handlers;
using ItemTrader.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.Proposals.Queries.Handlers
{
    public class GetProposalsWithPaginationQueryHandlerTests : HandlerTestBase
    {
        [SetUp]
        public void InitContextMock()
        {
            _context = new Mock<IApplicationDbContext>();
        }

        [Test]
        public async Task HandleShouldReturnAllProposalsWithEmptyQuery()
        {
            _context.Setup(c => c.Proposals).Returns(_proposals.Object);
            _mapper.Setup(x => x.ConfigurationProvider)
                .Returns(
                    () => new MapperConfiguration(
                        cfg =>
                        {
                            cfg.CreateMap<Proposal, ProposalDto>();
                            cfg.CreateMap<TradeItem, TradeItemDto>();
                        }));

            var getProposalsWithPaginationQuery = new GetProposalsWithPaginationQuery{OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4" };
            var handler = new GetProposalsWithPaginationQueryHandler(_context.Object, _mapper.Object);

            var proposals = await handler.Handle(getProposalsWithPaginationQuery, new CancellationToken());

            Assert.AreEqual(_proposals.Object.Count(),proposals.Items.Count);
        }
    }
}
