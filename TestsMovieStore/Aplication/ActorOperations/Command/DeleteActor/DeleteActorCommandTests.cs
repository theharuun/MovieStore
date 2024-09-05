using AutoMapper;
using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;
using static System.Net.Mime.MediaTypeNames;

namespace TestsMovieStore.Aplication.ActorOperations.Command.DeleteActor
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyNotExistActorIDIsGivenAndDeleted_InvalidOperationException_ShoulBeReturn()
        {
            //arrange
            var actor = new Actor()
            {
                ActorID = 13,
                Name = "Berkay",
                Surname = "Genceroğlu"
            };

            _context.Actors.Add(actor);
            _context.SaveChanges();

            Actor notExitActor = new Actor() { Name = "dsgwgrgr", Surname = "wgrwgrwg", ActorID = 4165 };

            //act
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.actorId = notExitActor.ActorID;
            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor not found-Oyuncu bulunamadi");

        }

        [Fact]
        public void WhenActorDeletedWhileThereAreMoviesHeorSheHasPlayedIn_InvalidOperationException_ShoulBeReturn()
        {
            //arrange
            var actor = new Actor()
            {
                ActorID = 13,
                Name = "Berkay",
                Surname = "Genceroğlu",
                MovieActors = { new MovieActor() { MovieID = 1 }, new MovieActor() { MovieID = 2 } }
            };

            _context.Actors.Add(actor);
            _context.SaveChanges();

            //act
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.actorId = actor.ActorID;

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There are movies this actor has played in. First, you must delete the movie. - Bu oyuncunun oynadigi filmler var. Öncelikle filmleri silmelisiniz.");

        }

        [Fact]  // happy path
        public void WhenValidInputasAreGiven_Validator_ShouldBeCreated()
        {
            //arrange
            var actor = new Actor()
            {
                ActorID = 13,
                Name = "Berkay",
                Surname = "Genceroğlu"
            };

            _context.Actors.Add(actor);
            _context.SaveChanges();

            //act
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.actorId = actor.ActorID;

            //act &assert
            FluentActions.Invoking(() => command.Handle())
                .Should().NotThrow();

        }
    }
}
