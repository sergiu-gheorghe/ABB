using FluentValidation;

namespace Application.Albums.Queries
{
    public class GetAlbumsQueryValidator : AbstractValidator<GetAlbumsQuery>
    {
        public GetAlbumsQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .MaximumLength(200);
        }
    }
}
