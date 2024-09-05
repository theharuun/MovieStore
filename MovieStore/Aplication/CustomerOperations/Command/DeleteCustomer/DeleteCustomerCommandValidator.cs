using FluentValidation;

namespace MovieStore.Aplication.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x=>x.email).NotEmpty().MinimumLength(18);
        }
    }
}
