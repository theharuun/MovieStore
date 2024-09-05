using FluentAssertions;
using MovieStore.Aplication.CustomerOperations.Command.UpdateCustomer;
using MovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsMovieStore.TestsSetup;

namespace TestsMovieStore.Aplication.CustomerOperations.Queries.GetCustomerByName
{
    public class GetCustomerByNameValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]  // Boş string
        [InlineData("a")] // 1 karakter
        [InlineData("ab")] // 2 karakter
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrorsTheory(string email)
        {
            //arrange
            GetCustomerByNameQuery command = new GetCustomerByNameQuery(null, null);
            command.email = email;
            //act
            GetCustomerByNameQueryValidator validationRules = new GetCustomerByNameQueryValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact] // happy path
        public void WhenValidGetCustomerByNameEmailIsThere_Validator_ShouldBeDeleted()
        {
            //arrange
            GetCustomerByNameQuery command = new GetCustomerByNameQuery(null, null);
            command.email = "johndoe@moviestore.com";
            //act
            GetCustomerByNameQueryValidator validationRules = new GetCustomerByNameQueryValidator();
            var result = validationRules.Validate(command);
            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
