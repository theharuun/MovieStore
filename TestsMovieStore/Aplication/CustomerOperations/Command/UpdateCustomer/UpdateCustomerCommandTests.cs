using AutoMapper;
using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.UpdateActor;
using MovieStore.Aplication.CustomerOperations.Command.UpdateCustomer;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Command.UpdateCustomer
{
    public class UpdateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistentCustomersEmailisGiven_InvalidOperationException_ShoulBeReturn()
        {
            //arrange
            var customer = new Customer()
            {
                Name = "Berkay",
                Surname = "Genceroğlu",
                 Email="berkaygenceroglu@moviestore.com"
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            //act
            UpdateCustomerCommand command = new UpdateCustomerCommand(_context, _mapper);
            command.email = "grwgrwgrwgsgrwgrwgrwgrgrgrgregrg";
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("This EMAIL has not customer - Bu EMAİL musteri yok");
        }

        [Fact]  // happy path
        public void WhenValidInputsAreGiven_Validator_ShouldBeCreated()
        {
            // Arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(_context, _mapper);
            command.email = "johndoe@moviestore.com";
            UpdateCustomerModel model = new UpdateCustomerModel()
            {
                purchasedMoviesIDs = new List<int> {  2, 3 }, favoriteGenresIDs= new List<int> { 1, 3 }
            };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var customer = _context.Customers.SingleOrDefault(x => x.Email == command.email);
            customer.Should().NotBeNull();
            customer.Email.Should().Be(command.email);
        }
    }
}
