using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using MovieStore.Aplication.ActorOperations.Queries.GetActorByID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsMovieStore.Aplication.ActorOperations.Queries.GetActorByID
{
    public class GetActorByIDValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(int actorId)
        {
            //arrange
            GetActorByIDQuery command = new GetActorByIDQuery(null, null);
            command.ID = actorId;
            //act
            GetActorByIDQueryValidator validationRules = new GetActorByIDQueryValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // happy path
        public void WhenValidDeletedActorIsThere_Validator_ShouldBeDeleted()
        {
            //arrange
            GetActorByIDQuery command = new GetActorByIDQuery(null, null);
            command.ID = 5;
            //act
            GetActorByIDQueryValidator validationRules = new GetActorByIDQueryValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
