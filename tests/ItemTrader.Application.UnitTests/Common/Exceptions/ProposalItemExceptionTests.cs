using ItemTrader.Application.Common.Exceptions;
using NUnit.Framework;

namespace ItemTrader.Application.UnitTests.Common.Exceptions
{
    public class ProposalItemExceptionTests
    {
        [Test]
        public void ConstructorWithMessageParameterShouldSetExceptionMessage()
        {
            var proposalItemException = new ProposalItemException("some message");
            Assert.AreEqual(proposalItemException.Message, "some message");
        }

        [Test]
        public void ConstructorWithItemAndKeyParametersShouldSetExceptionMessage()
        {
            var proposalItemException = new ProposalItemException("proposal", 4);
            Assert.AreEqual(proposalItemException.Message, "The proposal resource with id 4 does not belong to user.");
        }
    }
}
