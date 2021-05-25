using Application.Albums.Repositories;
using Domain;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Application.Common.Models;

namespace Application.Albums.Queries
{
    public class GetAlbumsQuery : PageRequest, IRequest<PaginatedList<Album>>
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
        public int? Id { get; set; }
    }

    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, PaginatedList<Album>>
    {
        private IAlbumRepository _albumRepository;

        public GetAlbumsQueryHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<PaginatedList<Album>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {
            var albums = await _albumRepository.GetAlbumsAsync();

            if(request.Id != null)
            {
                albums = albums.Where(album => album.Id == request.Id);
            }

            if(!string.IsNullOrWhiteSpace(request.Title))
            {
                albums = albums.Where(album => album.Title.Contains(request.Title));
            }

            if(request.UserId != null)
            {
                albums = albums.Where(album => album.UserId == request.UserId);
            }

            return PaginatedList<Album>.Create(albums, request.PageNumber, request.PageSize);
        }
    }
}
