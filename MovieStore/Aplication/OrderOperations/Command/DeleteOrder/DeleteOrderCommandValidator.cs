using FluentValidation;

namespace MovieStore.Aplication.OrderOperations.Command.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.orderId).NotEmpty().GreaterThan(0);
        }
    }
}
