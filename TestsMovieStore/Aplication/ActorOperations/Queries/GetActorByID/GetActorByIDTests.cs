using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using MovieStore.Aplication.ActorOperations.Queries.GetActorByID;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.ActorOperations.Queries.GetActorByID
{
    public class GetActorByIDTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorByIDTests(CommonTestFixture testFixture)
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
            GetActorByIDQuery command = new GetActorByIDQuery(_context, _mapper);
            command.ID = 20;
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor not found - Oyuncu bulunamadı");
        }
        [Fact]  // happy path
        public void WhenValidInputsAreGiven_Validator_ShouldBeCreated()
        {
            // Arrange
            GetActorByIDQuery command = new GetActorByIDQuery(_context, _mapper);
            command.ID = 10;
            GetActorByIdModel model = new GetActorByIdModel()
            {
                name = "Gene",
                surname = "Hackman",
                movies = new List<string> { "The Royal Tenenbaums" } // Listeyi bu şekilde oluşturmalısınız
            };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var actor = _context.Actors
                .Include(x => x.MovieActors)
                .ThenInclude(ma => ma.Movie)
                .Where(x => x.ActorID == command.ID)
                .SingleOrDefault();

            actor.Should().NotBeNull();
            actor.Name.Should().Be("Gene");
            actor.Surname.Should().Be("Hackman");

            // Film isimlerini kontrol etmek için:
            actor.MovieActors.Select(ma => ma.Movie.MovieName).Should().BeEquivalentTo(model.movies);

        }
    }
}
