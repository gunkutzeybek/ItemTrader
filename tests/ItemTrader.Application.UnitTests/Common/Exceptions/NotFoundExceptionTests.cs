using ItemTrader.Application.Common.Exceptions;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.Common.Exceptions
{
    public class NotFoundExceptionTests
    {
        [Test]
        public void ConstructorWithMessageParameterShouldSetExceptionMessage()
        {
            var notFoundException = new NotFoundException("some message");
            Assert.AreEqual(notFoundException.Message, "some message");
        }
    }
}
