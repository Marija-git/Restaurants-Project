
using FluentValidation.TestHelper;
namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Mexican",
                ContactEmail = "test@test.com",
                PostalCode = "11-111",
            };
            
           //act
           var result = validator.TestValidate(command); //metoda za validaciju objekata koji koriste FluentVal.bibl.

            //assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand()
            {
                Name = "a",
                Category = "mex",
                ContactEmail = "test.com",
                PostalCode = "11111",
            };

            //act
            var result = validator.TestValidate(command); 

            //assert
            result.ShouldHaveValidationErrorFor(c=>c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidName_ShouldNotHaveValidationErrorsForCategory(string category)
        {
            //arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { Category = category };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("111-11")]
        [InlineData("11 111")]
        [InlineData("11111")]
        [InlineData("11-1 11")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCode(string postalCode)
        {
            //arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalCode };
            //act
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}