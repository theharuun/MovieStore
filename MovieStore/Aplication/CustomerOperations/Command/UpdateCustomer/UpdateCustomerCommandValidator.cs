using FluentValidation;

namespace MovieStore.Aplication.CustomerOperations.Command.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator() 
        {
          RuleFor(x=>x.email).NotEmpty().MinimumLength(10);
        }
    }
}
