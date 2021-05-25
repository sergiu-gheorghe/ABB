using Application.Photos.Repositories;
using Domain;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Caching
{
    public class PhotoCachedRepository : IPhotoRepository
    {
        private PhotoRepository _photoRepository;
        private IAppCache _appCache;

        public PhotoCachedRepository(IAppCache appCache, PhotoRepository albumRepository)
        {
            _appCache = appCache;
            _photoRepository = albumRepository;
        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync()
        {
            Func<Task<IEnumerable<Photo>>> getPhotos = async () => await _photoRepository.GetPhotosAsync();

            // By default the cache expires after 20 minutes
            // Adding items is thread safe only one thread at a time can add items to the cache, others will wait
            // and will take data from cache
            // for details please see https://github.com/alastairtree/LazyCache
            return await _appCache.GetOrAddAsync("photos", getPhotos);
        }
    }
}
