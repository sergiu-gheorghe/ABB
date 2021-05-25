using Application.Albums.Repositories;
using Application.Photos.Repositories;
using Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private const string PhotosEndpointUrl = "https://jsonplaceholder.typicode.com/photos/";

        private HttpClient _httpClient;

        public PhotoRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Photo>>(PhotosEndpointUrl);
        }
    }
}
