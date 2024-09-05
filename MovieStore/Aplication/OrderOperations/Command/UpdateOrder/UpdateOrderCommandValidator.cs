using FluentValidation;

namespace MovieStore.Aplication.OrderOperations.Command.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x=>x.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}
