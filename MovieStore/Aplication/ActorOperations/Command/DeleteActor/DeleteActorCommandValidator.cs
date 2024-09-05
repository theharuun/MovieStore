using FluentValidation;

namespace MovieStore.Aplication.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator() 
        {
            RuleFor(x=>x.actorId).NotEmpty().GreaterThan(0);
        }
    }
}
