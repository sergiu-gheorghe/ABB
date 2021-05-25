using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Albums.Repositories
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetAlbumsAsync();
    }
}
