using FluentValidation;

namespace MovieStore.Aplication.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator() 
        {
            RuleFor(cmnd=>cmnd.Model.movieName).MinimumLength(4);
            RuleFor(cmnd=>cmnd.Model.genreID).NotEmpty().GreaterThan(0);
            RuleFor(cmnd => cmnd.Model.directorID).NotEmpty().GreaterThan(0);
            RuleFor(cmnd => cmnd.Model.movieDate).NotEmpty().LessThan(DateTime.Now);

        }
    }
}
