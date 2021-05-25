using Application.Albums.Repositories;
using Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        // TODO: move this to json config file
        private string AlbumsEndpoitUrl = "https://jsonplaceholder.typicode.com/albums/";

        private HttpClient _httpClient;

        public AlbumRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Album>>(AlbumsEndpoitUrl);
        }
    }
}
