using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.ActorOperations.Command.UpdateActor
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(int actorId)
        {
            //arrange
            UpdateActorCommand command = new UpdateActorCommand(null,null);
            command.actorId = actorId;
            //act
            UpdateActorCommandValidator validationRules = new UpdateActorCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // happy path
        public void WhenValidDeletedActorIsThere_Validator_ShouldBeDeleted()
        {
            //arrange
            UpdateActorCommand command = new UpdateActorCommand(null,null);
            command.actorId = 5;
            //act
            UpdateActorCommandValidator validationRules = new UpdateActorCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }

    }
}
