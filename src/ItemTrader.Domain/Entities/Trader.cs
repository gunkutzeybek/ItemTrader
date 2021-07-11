using System.Collections.Generic;

namespace ItemTrader.Domain.Entities
{
    public class Trader
    {
        private readonly string _userName;
        private readonly string _email;

        public Trader(string userName, string email)
        {
            _userName = userName;
            _email = email;
        }

        public string Id { get; }
        public string UserName => _userName;
        public string Email => _email;

        public List<TradeItem> TradeItems { get; set; }
        public List<Proposal> Proposals { get; set; }
        public List<Proposal> RecievedProposals { get; set; }
    }
}
