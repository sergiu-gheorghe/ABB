using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Photos.Repositories
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotosAsync();
    }
}
