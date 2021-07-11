using System;
using FluentValidation;
using FluentValidation.Results;
using ItemTrader.Application.Proposals.Commands;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ItemTrader.Application.Common.PipelineBehaviours;
using ItemTrader.Application.Proposals.Commands.Validators;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Application.TradeItems.Commands;
using ItemTrader.Application.TradeItems.Commands.Validators;
using ItemTrader.Application.TradeItems.Dto;
using MediatR;

namespace ItemTrader.Application.UnitTests.Common.PipelineBehaviours
{
    public class ValidateRequestBehaviourTests
    {
        public static IEnumerable<IGenericTestCase> TestCases()
        {
            yield return new GenericTestCase<UpdateProposalStatusCommand, UpdateProposalStatusCommandValidator, ProposalDto>();
            yield return new GenericTestCase<CreateProposalCommand, CreateProposalCommandValidator, ProposalDto>();
            yield return new GenericTestCase<CreateTradeItemCommand, CreateTradeItemCommandValidator, TradeItemDto>();
            yield return new GenericTestCase<DeleteTradeItemCommand, DeleteTradeItemCommandValidator, Unit>();
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ShouldThrowValidationExceptionWithAFailedValidation(IGenericTestCase testCase)
        {
            testCase.ShouldThrowValidationExceptionWithAFailedValidation();
        }
    }

    public interface IGenericTestCase
    {
        void ShouldThrowValidationExceptionWithAFailedValidation();
    }

    public class GenericTestCase<TCommand, TValidator, TResponse> : IGenericTestCase
        where TCommand : new()
        where TValidator : AbstractValidator<TCommand>, new()
    {
        private readonly Mock<RequestHandlerDelegate<TResponse>> _next = new Mock<RequestHandlerDelegate<TResponse>>();

        public void ShouldThrowValidationExceptionWithAFailedValidation()
        {
            var validator = new TValidator();
            var validateRequestBehaviour = new ValidateRequestBehaviour<TCommand, TResponse>(new List<IValidator<TCommand>> {validator});

            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await validateRequestBehaviour.Handle(new TCommand(),
                    new CancellationToken(),
                    _next.Object);
            });
        }
    } 
}
