using System;

namespace ItemTrader.Application.Common.Exceptions
{
    public class ProposalItemException : Exception
    {
        public ProposalItemException(string message)
            : base(message)
        {
        }

        public ProposalItemException(string name, object key)
            : base($"The {name} resource with id {key} does not belong to user.")
        {
        }
    }
}
