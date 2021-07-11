using AutoMapper;
using ItemTrader.Application.Common.Mappings;
using ItemTrader.Application.TradeItems.Dto;
using ItemTrader.Domain.Entities;

namespace ItemTrader.Application.Proposals.Dto
{
    public class ProposalDto : IMapFrom<Proposal>
    {
        public int Id { get; set; }
        public TradeItemDto OfferedItem { get; set; }
        public TradeItemDto RequestedItem { get; set; }
        public string OwnerId { get; set; }
        public string ProposedToId { get; set; }
        public string OwnerName { get; set; }
        public int Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Proposal, ProposalDto>()
                .ForMember(m => m.Status, opt => opt.MapFrom(s => (int)s.Status))
                .ForMember(m => m.OwnerName, opt => opt.MapFrom(s => s.Owner.UserName));
        }
    }
}
