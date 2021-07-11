using AutoMapper;
using ItemTrader.Application.Common.Mappings;
using ItemTrader.Domain.Entities;

namespace ItemTrader.Application.TradeItems.Dto
{
    public class TradeItemDto : IMapFrom<TradeItem>
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TradeItem, TradeItemDto>()
                .ForMember(m => m.Status, opt => opt.MapFrom(s => (int) s.Status))
                .ForMember(m => m.UserName, opt => opt.MapFrom(s => s.Owner.UserName));
        }
    }
}
