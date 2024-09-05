using MovieStore.Aplication.ActorOperations.Command.CreateActor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;
using FluentAssertions;
using System.Xml.Linq;

namespace TestsMovieStore.Aplication.ActorOperations.Command.CreateActor
{
    public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", new int[] { })]
        [InlineData("", "abcs", new int[] { 1 })]
        [InlineData("", "abcs", new int[] { })]
        [InlineData("", "", new int[] { 1 })]
        [InlineData("abcs", "", new int[] { 1 })]
        [InlineData("abcs", "", new int[] { })]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(string name, string surname, int[] movieIds)
        {

            List<int> movieList = movieIds.ToList();

            //arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            CreateActorModel model = new CreateActorModel()
            {
                name = name,
                surname = surname,
                moviesIDs = movieList
            };
            command.Model = model;

            // act
            CreateActorCommandValidator validationRules = new CreateActorCommandValidator();
            var result = validationRules.Validate(command);

            //asset
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact] //Happy path doğru girişler
        public void WhenValidInputasAreGiven_Validator_ShouldBeNotReturnErrors()
        {      //arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            CreateActorModel model = new CreateActorModel()
            {
                name = "harun",
                surname = "fgmşlrgrg",
                moviesIDs = [1, 2]
            };
            command.Model = model;

            // act
            CreateActorCommandValidator validationRules = new CreateActorCommandValidator();
            var result = validationRules.Validate(command);

            //asset
            result.Errors.Count.Should().Be(0);

        }


    }
}
