using Application.Users.Repositories;
using Domain;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Caching
{
    public class UserCachedRepository : IUserRepository
    {
        private UserRepository _userRepository;
        private IAppCache _appCache;

        public UserCachedRepository(IAppCache appCache, UserRepository userRepository)
        {
            _appCache = appCache;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            Func<Task<IEnumerable<User>>> getUsers = async () => await _userRepository.GetUsersAsync();

            // By default the cache expires after 20 minutes
            // Adding items is thread safe only one thread at a time can add items to the cache, others will wait
            // and will take data from cache
            // for details please see https://github.com/alastairtree/LazyCache
            return await _appCache.GetOrAddAsync("users", getUsers);
        }
    }
}
