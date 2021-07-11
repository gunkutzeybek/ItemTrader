using FluentValidation;

namespace ItemTrader.Application.TradeItems.Commands.Validators
{
    public class CreateTradeItemCommandValidator : AbstractValidator<CreateTradeItemCommand>
    {
        public CreateTradeItemCommandValidator()
        {
            RuleFor(v => v.OwnerId)
                .NotEmpty();

            RuleFor(v => v.Name)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
