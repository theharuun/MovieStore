using FluentValidation;

namespace MovieStore.Aplication.MovieOperations.Command.DeleteMovie
{
    public class DeleteCommandMovieValidator: AbstractValidator<DeleteCommandMovie>
    {
        public DeleteCommandMovieValidator() 
        {
            RuleFor(x=>x.movieId).NotEmpty().GreaterThan(0).NotNull();
        }
    }
}
