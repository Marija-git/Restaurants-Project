using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator:AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Invalid category. Choose from the valid categories.");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Provide a valid email address.");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Provide a valid postal code (XX-XXX)");
        }
    }
}
