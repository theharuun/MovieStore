using FluentValidation;

namespace MovieStore.Aplication.MovieOperations.Command.UpdateMovie
{
    public class UpdateCommandMovieValidator : AbstractValidator<UpdateCommandMovie>
    {
        public UpdateCommandMovieValidator() 
        {
            RuleFor(cmnd => cmnd.Model.movieName).NotEmpty().MinimumLength(4);
            RuleFor(cmnd => cmnd.Model.genreID).NotEmpty().GreaterThan(0);
            RuleFor(cmnd => cmnd.Model.directorID).NotEmpty().GreaterThan(0);
            RuleFor(cmnd => cmnd.Model.movieDate).NotEmpty().LessThan(DateTime.Now);

        }
    }
}
