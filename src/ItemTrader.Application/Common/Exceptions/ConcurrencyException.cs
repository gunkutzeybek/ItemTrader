using System;

namespace ItemTrader.Application.Common.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message)
            : base(message)
        {
        }
    }
}
