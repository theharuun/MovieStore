using AutoMapper;
using MovieStore.Aplication.ActorOperations.Command.CreateActor;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;
using FluentAssertions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace TestsMovieStore.Aplication.ActorOperations.Command.CreateActor
{
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistActorNameandSurnameIsGiven_InvalidOperationException_ShoulBeReturn()
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

            CreateActorCommand cmnd= new CreateActorCommand(_context, _mapper);
            cmnd.Model= new CreateActorModel() { name=actor.Name, surname=actor.Surname, moviesIDs = [1,2] };

            //act&assert
            // act & assert
            FluentActions
                .Invoking(() => cmnd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is already this actor-Bu oyuncu zaten var");

        }
        [Fact]  // happy path
        public void WhenValidInputasAreGiven_Validator_ShouldBeCreated()
        {
            //arrange
            CreateActorCommand command = new CreateActorCommand(_context,_mapper);
            CreateActorModel model = new CreateActorModel() { name="hakan",surname="peker", moviesIDs = [1,2] };
            command.Model = model;
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //assert
            var actor = _context.Actors.Include(x => x.MovieActors).ThenInclude(ma => ma.Movie).SingleOrDefault(x => x.Name == model.name && x.Surname == model.surname);
            actor.Should().NotBeNull();
            actor.Name.Should().Be(model.name);
            actor.Surname.Should().Be(model.surname);
        }
     }
}
