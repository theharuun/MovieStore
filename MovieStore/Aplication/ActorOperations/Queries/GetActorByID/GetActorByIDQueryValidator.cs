using FluentValidation;

namespace MovieStore.Aplication.ActorOperations.Queries.GetActorByID
{
    public class GetActorByIDQueryValidator : AbstractValidator<GetActorByIDQuery>
    {
        public GetActorByIDQueryValidator() 
        {
            RuleFor(x=>x.ID).NotEmpty().GreaterThan(0);
        }
    }
}
