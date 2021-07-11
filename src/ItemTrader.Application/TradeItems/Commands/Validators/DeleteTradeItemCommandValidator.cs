using FluentValidation;

namespace ItemTrader.Application.TradeItems.Commands.Validators
{
    public class DeleteTradeItemCommandValidator : AbstractValidator<DeleteTradeItemCommand>
    {
        public DeleteTradeItemCommandValidator()
        {
            RuleFor(c => c.OwnerId)
                .MaximumLength(450)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.TradeItemId)
                .NotEmpty();
        }
    }
}
