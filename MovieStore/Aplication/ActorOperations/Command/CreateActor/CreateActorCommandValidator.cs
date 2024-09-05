using FluentValidation;

namespace MovieStore.Aplication.ActorOperations.Command.CreateActor
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator() {
        RuleFor(s=>s.Model.name).NotEmpty().MinimumLength(3);
        RuleFor(s => s.Model.surname).NotEmpty().MinimumLength(3);
        RuleFor(s => s.Model.moviesIDs).NotEmpty();
        }
    }
}
