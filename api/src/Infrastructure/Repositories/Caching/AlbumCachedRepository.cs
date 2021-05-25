using Application.Albums.Repositories;
using Domain;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Caching
{
    public class AlbumCachedRepository : IAlbumRepository
    {
        private AlbumRepository _albumRepository;
        private IAppCache _appCache;

        public AlbumCachedRepository(IAppCache appCache, AlbumRepository albumRepository)
        {
            _appCache = appCache;
            _albumRepository = albumRepository;
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            Func<Task<IEnumerable<Album>>> getAlbums = async () => await _albumRepository.GetAlbumsAsync();

            // By default the cache expires after 20 minutes
            // Adding items is thread safe only one thread at a time can add items to the cache, others will wait
            // and will take data from cache
            // for details please see https://github.com/alastairtree/LazyCache
            return await _appCache.GetOrAddAsync("albums", getAlbums);
        }
    }
}
