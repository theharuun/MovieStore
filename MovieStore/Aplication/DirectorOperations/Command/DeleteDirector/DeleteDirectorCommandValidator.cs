using FluentValidation;

namespace MovieStore.Aplication.DirectorOperations.Command.DeleteDirector
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator() 
        {
            RuleFor(s=>s.directorId).NotEmpty().GreaterThan(0);
        }
    }
}
