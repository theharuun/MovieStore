using FluentAssertions;
using MovieStore.Aplication.CustomerOperations.Command.DeleteCustomer;
using MovieStore.Aplication.CustomerOperations.Command.UpdateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Command.UpdateCustomer
{
    public class UpdateCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]  // Boş string
        [InlineData("a")] // 1 karakter
        [InlineData("ab")] // 2 karakter
        [InlineData("abc")] // 3 karakter
        [InlineData("abcd")] // 4 karakter
        [InlineData("abcde")] // 5 karakter
        [InlineData("abcdef")] // 6 karakter
        [InlineData("abcdefg")] // 7 karakter
        [InlineData("abcdefgh")] // 8 karakter
        [InlineData("abcdefghi")] // 9 karakter
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(string email)
        {
            //arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(null,null);
            command.email = email;
            //act
            UpdateCustomerCommandValidator validationRules = new UpdateCustomerCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // happy path
        public void WhenValidDeletedCustomerIsThere_Validator_ShouldBeDeleted()
        {
            //arrange
            UpdateCustomerCommand command = new UpdateCustomerCommand(null,null);
            command.email = "johndoe@moviestore.com";
            //act
            UpdateCustomerCommandValidator validationRules = new UpdateCustomerCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
