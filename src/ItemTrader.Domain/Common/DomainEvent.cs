using System;

namespace ItemTrader.Domain.Common
{
    public class DomainEvent
    {
        public bool IsPublished { get; set; }
        public DateTimeOffset EventDate { get; protected set; }
        protected DomainEvent()
        {
            EventDate = DateTimeOffset.UtcNow;
        }
    }
}
