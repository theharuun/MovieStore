using FluentValidation;

namespace MovieStore.Aplication.MovieOperations.Queries.GetMovieByID
{
    public class GetMoviesByIDQueryValidator : AbstractValidator <GetMoviesByIDQuery>
    {
        public GetMoviesByIDQueryValidator()
        {
            RuleFor(s=>s.ID).NotEmpty().GreaterThan(0);
        }
    }
}
