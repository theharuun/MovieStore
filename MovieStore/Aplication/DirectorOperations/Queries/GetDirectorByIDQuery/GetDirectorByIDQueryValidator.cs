using FluentValidation;

namespace MovieStore.Aplication.DirectorOperations.Queries.GetDirectorByIDQuery
{
    public class GetDirectorByIDQueryValidator : AbstractValidator<GetDirectorByIDQuery>
    {
        public GetDirectorByIDQueryValidator() 
        {
            RuleFor(x=>x.ID).NotEmpty().GreaterThan(0);
        }
    }
}
