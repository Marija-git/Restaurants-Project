using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] pageSizes = [3, 6, 10];
        private string[] sortByAllowdValues = [nameof(RestaurantDto.Name),
                                               nameof(RestaurantDto.Description),
                                               nameof(RestaurantDto.Category)];
        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageIndex)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => pageSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(",", pageSizes)}]");

            RuleFor(r => r.SortBy)
                .Must(value => sortByAllowdValues.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"SortBy is optional or must be in [{string.Join(",", sortByAllowdValues)}]");

        }
    }
}
