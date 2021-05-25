using FluentValidation;

namespace Application.Users.Queries
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
            RuleFor(x => x.Name)
                .MaximumLength(200);
        }
    }
}
