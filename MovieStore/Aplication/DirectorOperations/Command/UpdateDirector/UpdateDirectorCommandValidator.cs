using FluentValidation;

namespace MovieStore.Aplication.DirectorOperations.Command.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator() 
        {
            RuleFor(s=>s.ID).NotEmpty().GreaterThan(0);
        }
    }
}
