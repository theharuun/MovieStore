using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.ActorOperations.Queries.GetActorByID;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName
{
    public class GetCustomerByNameTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerByNameTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistentCustumerEmailisGiven_InvalidOperationException_ShoulBeReturn()
        {
            //arrange
            var custumer = new Customer()
            {
                Email="berkaygenceroglu@moviestore.com",
                Name = "Berkay",
                Surname = "Genceroğlu"
            };

            _context.Customers.Add(custumer);
            _context.SaveChanges();
            //act
            GetCustomerByNameQuery command = new GetCustomerByNameQuery(_context, _mapper);
            command.email = "fsgrrehrherhrhrhrhhhherrgr";
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Musteri BULUNAMADI!!!--Customer NOT FOUND!!!");
        }
        [Fact]  // happy path
        public void WhenValidInputsAreGiven_Validator_ShouldBeCreated()
        {
            // Arrange
            GetCustomerByNameQuery command = new GetCustomerByNameQuery(_context, _mapper);
            command.email = "johndoe@moviestore.com";
            GetCustomerByNameModel model = new GetCustomerByNameModel()
            { Email= "johndoe@moviestore.com", Name="John", Surname="Doe" };
               

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var customer = _context.Customers
          .Include(c => c.purchasedMovies)
          .ThenInclude(pm => pm.Movie)
          .Include(c => c.favoriteGenres)
          .ThenInclude(fg => fg.Genre)
          .Where(x => x.Email == command.email)
          .SingleOrDefault();

            customer.Should().NotBeNull();

            customer.Name.Should().Be(model.Name);
            customer.Surname.Should().Be(model.Surname);
            customer.Email.Should().Be(model.Email);
            

        }
    }
}
