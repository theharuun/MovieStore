using FluentValidation;

namespace MovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName
{
    public class GetCustomerByNameQueryValidator : AbstractValidator<GetCustomerByNameQuery>
    {
        public GetCustomerByNameQueryValidator() 
        {
            RuleFor(s=>s.email).NotEmpty().MinimumLength(3);
        }
    }
}
