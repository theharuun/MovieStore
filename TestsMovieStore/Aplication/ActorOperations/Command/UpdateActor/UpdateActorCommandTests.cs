using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.ActorOperations.Command.CreateActor;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.ActorOperations.Command.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenNotExistentActorIDisGiven_InvalidOperationException_ShoulBeReturn()
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
            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            command.actorId = 20;
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This ID  has not Actor - Bu ID oyuncu yok");
        }

        [Fact]  // happy path
        public void WhenValidInputsAreGiven_Validator_ShouldBeCreated()
        {
            // Arrange
            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            command.actorId = 10;
            UpdateActorModel model = new UpdateActorModel()
            {
                moviesIDs = new List<int> { 1, 2, 3 }
            };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var actor = _context.Actors.SingleOrDefault(x => x.ActorID == command.actorId);
            actor.Should().NotBeNull();

            // Bu kısımda actor.Name yerine actor'un moviesIDs özelliğini kontrol etmelisiniz.
            actor.MovieActors.Select(m => m.MovieID).Should().BeEquivalentTo(model.moviesIDs);
        }

    }
}
