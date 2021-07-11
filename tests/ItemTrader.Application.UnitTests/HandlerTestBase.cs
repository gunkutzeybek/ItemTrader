using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ItemTrader.Application.Common.Interfaces;
using ItemTrader.Domain.Entities;
using ItemTrader.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace ItemTrader.Application.UnitTests
{
    public abstract class HandlerTestBase
    {
        protected Mock<DbSet<TradeItem>> _tradeItems;
        protected Mock<DbSet<Proposal>> _proposals;
        protected Mock<IApplicationDbContext> _context;
        protected readonly Mock<IMapper> _mapper;
        protected readonly IQueryable<TradeItem> _tradeItemData;
        protected readonly IQueryable<Proposal> _proposalsData;

        protected HandlerTestBase()
        {
            _mapper = new Mock<IMapper>();
            var alice = new Trader("alice", "alice@example.com");
            var bob = new Trader("bob", "bob@example.com");
            _tradeItemData = new List<TradeItem>
            {
                new TradeItem { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = TradeItemStatus.Listed, Name = "telefon", Id = 1, Owner = bob},
                new TradeItem { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = TradeItemStatus.Listed, Name = "kırbaç", Id = 2, Owner = bob},
                new TradeItem { OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", Status = TradeItemStatus.Listed, Name = "kırbaç", Id = 3, Owner = alice},
                new TradeItem { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = TradeItemStatus.Traded, Name = "kırbaç", Id = 6, Owner = bob},
                new TradeItem {OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", Status = TradeItemStatus.Listed, Name = "kaplumbağa", Id = 4, Owner = alice},
                new TradeItem {OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", Status = TradeItemStatus.InProposal, Name = "masa", Id = 5, Owner = alice},
                new TradeItem {OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", Status = TradeItemStatus.Traded, Name = "masa", Id = 7, Owner = alice},
            }.AsQueryable();

            _proposalsData = new List<Proposal>
            {
                new Proposal
                {
                    OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", ProposedToId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = ProposalStatus.Cancelled, OfferedItemId = 4, RequestedItemId = 2, Id = 1,
                    OfferedItem = new TradeItem {OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", Status = TradeItemStatus.Listed, Name = "kaplumbağa", Id = 4},
                    RequestedItem = new TradeItem { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = TradeItemStatus.Listed, Name = "kırbaç", Id = 2}
                },
                new Proposal
                {
                    OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", ProposedToId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = ProposalStatus.Active, OfferedItemId = 4, RequestedItemId = 2, Id = 2,
                    OfferedItem = new TradeItem {OwnerId = "9c784a12-50ca-4869-95fe-15b00071efb4", Status = TradeItemStatus.Listed, Name = "kaplumbağa", Id = 4},
                    RequestedItem = new TradeItem { OwnerId = "3813d77b-e04e-4a12-a036-fab1de9d16fb", Status = TradeItemStatus.Listed, Name = "kırbaç", Id = 2}
                }
            }.AsQueryable();

            _tradeItems = _tradeItemData.BuildMockDbSet();

            _proposals = _proposalsData.BuildMockDbSet();
        }
    }
}
