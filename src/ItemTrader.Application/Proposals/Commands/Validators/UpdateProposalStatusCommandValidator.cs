using System;
using FluentValidation;
using ItemTrader.Domain.Enums;

namespace ItemTrader.Application.Proposals.Commands.Validators
{
    public class UpdateProposalStatusCommandValidator : AbstractValidator<UpdateProposalStatusCommand>
    {
        public UpdateProposalStatusCommandValidator()
        {
            RuleFor(c => c.OwnerId)
                .MaximumLength(450)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.ProposalId)
                .GreaterThan(0);

            RuleFor(c => c.Status)
                .Must(c => Enum.IsDefined(typeof(ProposalStatus), c));
        }
    }
}
