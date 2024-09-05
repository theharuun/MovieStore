using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;

namespace MovieStore.Aplication.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommandValidator: AbstractValidator<CreateCustomerCommand>
    {
        private readonly IMovieStoreDbContext _dbContext;
        public CreateCustomerCommandValidator(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Model.name).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Model.surname).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Model.email).NotEmpty().MinimumLength(4).Must(BeUniqueEmail).WithMessage("this email is already in use");
            
        }
        private bool BeUniqueEmail(string email)
        {
            return !_dbContext.Customers.Any(c => c.Email == email);
        }
    }
}
 