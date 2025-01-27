using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dto => dto.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price nmust be a non-negative number;");

            RuleFor(dto => dto.KiloCalories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("KiloCalories must me a non-negative number.");

        }
    }
}
