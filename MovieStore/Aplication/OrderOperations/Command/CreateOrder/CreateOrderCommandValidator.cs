using FluentValidation;

namespace MovieStore.Aplication.OrderOperations.Command.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
             RuleFor(x=>x.Model.customerID).NotEmpty();
        }
    }
}
