using AutoMapper;
using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.Aplication.CustomerOperations.Command.DeleteCustomer;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyNotExistCustomerEmailIsGivenToDelete_InvalidOperationException_ShoulBeReturn()
        {
            var customer = new Customer() { Email = "berkaygenceroglu@moviestore.com", CustomerID = 3, Name = "berkay", Surname = "genceroglu" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            Customer notExitActor = new Customer() { Email = "errngerklgrkrgkregmrgşrl", Name = "rwgreergr", Surname = "efbfegergr" };


            //act
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.email = notExitActor.Email;
            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer not found-Musteri bulunamadi");
        }


        [Fact] // happy path
        public void WhenAlreadyExistCustomerEmailIsGivenToDelete_InvalidOperationException_ShoulBeReturn()
        {
            var customer = new Customer() { Email = "berkaygenceroglu@moviestore.com", CustomerID = 3, Name = "berkay", Surname = "genceroglu" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

      


            //act
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.email = customer.Email;

            //act &assert
            FluentActions.Invoking(() => command.Handle())
                .Should().NotThrow();
        }
    }
}
