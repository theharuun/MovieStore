using FluentAssertions;
using MovieStore.Aplication.ActorOperations.Command.DeleteActor;
using MovieStore.Aplication.CustomerOperations.Command.DeleteCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Command.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests : IClassFixture<CommonTestFixture>
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
        [InlineData("abcdefghij")] // 10 karakter
        [InlineData("abcdefghijk")] // 11 karakter
        [InlineData("abcdefghijkl")] // 12 karakter
        [InlineData("abcdefghijklm")] // 13 karakter
        [InlineData("abcdefghijklmn")] // 14 karakter
        [InlineData("abcdefghijklmno")] // 15 karakter
        [InlineData("abcdefghijklmnop")] // 16 karakter
        [InlineData("abcdefghijklmnopq")] // 17 karakter (Minimum 18 karakter olmalı)

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(string email)
        {
            //arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.email = email;
            //act
            DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // happy path
        public void WhenValidDeletedCustomerIsThere_Validator_ShouldBeDeleted()
        {
            //arrange
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.email = "johndoe@moviestore.com";
            //act
            DeleteCustomerCommandValidator validationRules = new DeleteCustomerCommandValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
