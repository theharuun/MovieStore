using FluentValidation;

namespace MovieStore.Aplication.OrderOperations.Queries.GetOrderByID
{
    public class GetOrderByIDQueryValidator : AbstractValidator<GetOrderByIDQuery>
    {
        public GetOrderByIDQueryValidator()
        {
            RuleFor(x=>x.ID).NotEmpty().GreaterThan(0);
        }
    }
}
