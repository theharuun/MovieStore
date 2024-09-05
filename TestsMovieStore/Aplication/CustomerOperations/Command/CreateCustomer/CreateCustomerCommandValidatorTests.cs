using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Aplication.CustomerOperations.Command.CreateCustomer;
using MovieStore.DbOperations;
using MovieStore.Entities;

public class CreateCustomerCommandValidatorTests
{
    [Theory]
    [InlineData("", "", "")]
    [InlineData("", "abcs", "abcs")]
    [InlineData("", "abcs", "")]
    [InlineData("", "", "abcs")]
    [InlineData("abcs", "", "")]
    [InlineData("abcs", "", "abcs")]
    [InlineData("abcs", "abcs", "")]
    [InlineData("abcs", "", "")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string surname, string email)
    {
        // Arrange
        var customers = new List<Customer>
        {
            new Customer { Email = "test1@example.com" },
            new Customer { Email = "test2@example.com" }
        }.AsQueryable();

        var mockDbSet = new Mock<DbSet<Customer>>();
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());

        var mockDbContext = new Mock<IMovieStoreDbContext>();
        mockDbContext.Setup(c => c.Customers).Returns(mockDbSet.Object);

        CreateCustomerCommand command = new CreateCustomerCommand(mockDbContext.Object, null);
        CreateCustomerModel model = new CreateCustomerModel()
        {
            name = name,
            surname = surname,
            email = email
        };
        command.Model = model;

        // Act
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator(mockDbContext.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnErrors()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new Customer { Email = "existinguser@example.com" }
        }.AsQueryable();

        var mockDbSet = new Mock<DbSet<Customer>>();
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
        mockDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());

        var mockDbContext = new Mock<IMovieStoreDbContext>();
        mockDbContext.Setup(c => c.Customers).Returns(mockDbSet.Object);

        // Geçerli kullanıcı bilgileri
        CreateCustomerCommand command = new CreateCustomerCommand(mockDbContext.Object, null);
        CreateCustomerModel model = new CreateCustomerModel()
        {
            name = "John",
            surname = "Doe22",
            email = "johndoe22@example.com" // Benzersiz bir email
        };
        command.Model = model;

        // Act
        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator(mockDbContext.Object);
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0); // Hata olmamalı
    }
}
