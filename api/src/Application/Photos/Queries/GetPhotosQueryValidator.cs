using FluentValidation;

namespace Application.Photos.Queries
{
    public class GetPhotosQueryValidator : AbstractValidator<GetPhotosQuery>
    {
        public GetPhotosQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.AlbumId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .MaximumLength(200);
        }
    }
}
