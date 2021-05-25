using Api;
using Application.Users.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net.Http;
using Moq;
using Domain;
using System.Collections.Generic;
using Application.Albums.Repositories;
using Application.Photos.Repositories;

namespace IntegrationTests
{
    [SetUpFixture]
    public class TestsSetup
    {
        private static WebApplicationFactory<Startup> _webApplicationFactory;

        private static Mock<IAlbumRepository> albumRepositoryMock = new Mock<IAlbumRepository>();
        private static Mock<IPhotoRepository> photoRepositoryMock = new Mock<IPhotoRepository>();
        private static Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

        public static HttpClient HttpClient { get; private set; }

        [OneTimeSetUp]
        public void RunBeforeAnyTestsAsync()
        {
            SetUpWebApplicationFactory();
        }

        private void SetUpWebApplicationFactory()
        {
            _webApplicationFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Mock the repositories using Moq
                    services.AddTransient(factory => albumRepositoryMock.Object);
                    services.AddTransient(factory => photoRepositoryMock.Object);
                    services.AddTransient(factory => userRepositoryMock.Object);
                });
            });

            HttpClient = _webApplicationFactory.CreateClient();
        }

        public static void SetUpAlbums(IEnumerable<Album> albums)
        {
            albumRepositoryMock.Setup(a => a.GetAlbumsAsync()).ReturnsAsync(albums);
        }

        public static void SetUpPhotos(IEnumerable<Photo> photos)
        {
            photoRepositoryMock.Setup(f => f.GetPhotosAsync()).ReturnsAsync(photos);
        }

        public static void SetUpUsers(IEnumerable<User> users)
        {
            userRepositoryMock.Setup(u => u.GetUsersAsync()).ReturnsAsync(users);
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            if (HttpClient != null)
            {
                HttpClient.Dispose();
            }

            if (_webApplicationFactory != null)
            {
                _webApplicationFactory.Dispose();
            }
        }
    }
}
