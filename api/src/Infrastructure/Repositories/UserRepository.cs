using Application.Users.Repositories;
using Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        // TODO: move this to json config file
        private const string UsersEndpointUrl = "https://jsonplaceholder.typicode.com/users/";

        private HttpClient _httpClient;

        public UserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>(UsersEndpointUrl);
        }
    }
}
