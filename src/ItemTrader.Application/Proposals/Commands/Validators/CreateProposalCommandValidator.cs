using FluentValidation;
using FluentValidation.Validators;

namespace ItemTrader.Application.Proposals.Commands.Validators
{
    public class CreateProposalCommandValidator : AbstractValidator<CreateProposalCommand>
    {
        public CreateProposalCommandValidator()
        {
            RuleFor(c => c.OfferedItemId)
                .GreaterThan(0);

            RuleFor(c => c.OwnerId)
                .MaximumLength(450)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.RequestedItemId)
                .GreaterThan(0);
        }
    }
}
