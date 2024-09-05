using AutoMapper;
using Castle.Core.Resource;
using FluentAssertions;
using MovieStore.Aplication.CustomerOperations.Command.CreateCustomer;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Command.CreateCustomer
{
    public class CreateCustomerCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistCustomerEmailIsGiven_InvalidOperationException_ShoulBeReturn()
        {
            var customer = new Customer() { Email = "berkaygenceroglu@moviestore.com", CustomerID = 3, Name = "berkay", Surname = "genceroglu" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = new CreateCustomerModel() { email = customer.Email, name = customer.Name, surname = customer.Surname };

            //act&assert
            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu emaile sahip musteri var- there is a customer with same email");
        }

        [Fact]  // happy path
        public void WhenValidInputasAreGiven_Validator_ShouldBeCreated()
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            CreateCustomerModel model = new CreateCustomerModel() { email = "berkaygenceroglu@moviestore.com", name = "berkay", surname ="genceroglu" };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var customer = _context.Customers.SingleOrDefault(x => x.Email == command.Model.email);
            customer.Should().NotBeNull();
            customer.Name.Should().Be(model.name);
            customer.Surname.Should().Be(model.surname);
        }
    }
}
