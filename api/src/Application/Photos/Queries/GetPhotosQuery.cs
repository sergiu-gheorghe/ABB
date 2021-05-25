using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Application.Photos.Repositories;
using Application.Common.Models;

namespace Application.Photos.Queries
{
    public class GetPhotosQuery : PageRequest, IRequest<PaginatedList<Photo>>
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int? AlbumId { get; set; }
    }

    public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQuery, PaginatedList<Photo>>
    {
        private IPhotoRepository _photoRepository;
        
        public GetPhotosQueryHandler(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<PaginatedList<Photo>> Handle(GetPhotosQuery request, CancellationToken cancellationToken)
        {
            var photos = await _photoRepository.GetPhotosAsync();

            if (request.Id != null)
            {
                photos = photos.Where(photo => photo.Id == request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                photos = photos.Where(photo => photo.Title.Contains(request.Title));
            }

            if (request.AlbumId != null)
            {
                photos = photos.Where(photo => photo.AlbumId == request.AlbumId);
            }

            return PaginatedList<Photo>.Create(photos, request.PageNumber, request.PageSize);
        }
    }
}
