using FluentValidation;

namespace MovieStore.Aplication.DirectorOperations.Command.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    { 
        public CreateDirectorCommandValidator() 
        {
            RuleFor(s=>s.Model.name).NotEmpty().MinimumLength(3);
            RuleFor(s=>s.Model.surname).NotEmpty().MinimumLength(3);
        }
    }
}
