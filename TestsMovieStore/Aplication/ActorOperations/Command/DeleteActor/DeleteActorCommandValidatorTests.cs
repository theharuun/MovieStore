using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-11)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(int actorId)
        {
            //arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.actorId = actorId;
            //act
            DeleteActorCommandValidator validationRules = new DeleteActorCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // happy path
        public void WhenValidDeletedActorIsThere_Validator_ShouldBeDeleted()
        {
            //arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.actorId = 5;
            //act
            DeleteActorCommandValidator validationRules = new DeleteActorCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
