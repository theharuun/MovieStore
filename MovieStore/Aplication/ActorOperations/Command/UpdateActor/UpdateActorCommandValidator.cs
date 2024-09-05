using FluentValidation;

namespace MovieStore.Aplication.ActorOperations.Command.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator() 
        {
            RuleFor(x => x.actorId).NotEmpty().GreaterThan(0);
        }
    }
}
