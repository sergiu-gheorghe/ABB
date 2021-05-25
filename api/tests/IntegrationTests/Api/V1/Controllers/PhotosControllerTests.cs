using Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using System.Linq;
using AutoFixture;

namespace IntegrationTests.Api.V1.Controllers
{
    using static TestsSetup;

    public class PhotosControllerTests
    {
        private const string PhotosRequestUri = "api/v1/photos";

        private Fixture fixture = new Fixture();

        [Test]
        public async Task ShouldGetAllPhotos()
        {
            // Arrange
            var expectedPhotos = fixture.CreateMany<Photo>(10).ToList();
            SetUpPhotos(expectedPhotos);

            // Act 
            var result = await HttpClient.GetAsync(PhotosRequestUri);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Photo>>();

            paginatedResult.TotalCount.Should().Be(expectedPhotos.Count);
            paginatedResult.TotalPages.Should().Be(1);

            for (int i = 0; i < paginatedResult.Items.Count; i++)
            {
                paginatedResult.Items[i].Id.Should().Be(expectedPhotos[i].Id);
                paginatedResult.Items[i].Title.Should().Be(expectedPhotos[i].Title);
                paginatedResult.Items[i].AlbumId.Should().Be(expectedPhotos[i].AlbumId);
            }
        }

        [Test]
        public async Task ShouldGetPhotosFilteredByAlbumId()
        {
            // Arrange
            var expectedPhotos = fixture.CreateMany<Photo>(10).ToList();
            expectedPhotos[1].AlbumId = 1;

            SetUpPhotos(expectedPhotos);

            // Act 
            var result = await HttpClient.GetAsync($"{PhotosRequestUri}?albumId=1");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Photo>>();

            paginatedResult.TotalCount.Should().Be(1);
            paginatedResult.TotalPages.Should().Be(1);

            paginatedResult.Items[0].Id.Should().Be(expectedPhotos[1].Id);
            paginatedResult.Items[0].Title.Should().Be(expectedPhotos[1].Title);
            paginatedResult.Items[0].AlbumId.Should().Be(expectedPhotos[1].AlbumId);
        }

        [Test]
        public async Task ShouldGetPhotosFilteredByTitle()
        {
            // Arrange
            var expectedPhotos = fixture.CreateMany<Photo>(10).ToList();
            expectedPhotos[5].Title = "testing";

            SetUpPhotos(expectedPhotos);

            // Act 
            var result = await HttpClient.GetAsync($"{PhotosRequestUri}?title=testing");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Photo>>();

            paginatedResult.TotalCount.Should().Be(1);
            paginatedResult.TotalPages.Should().Be(1);

            paginatedResult.Items[0].Id.Should().Be(expectedPhotos[5].Id);
            paginatedResult.Items[0].Title.Should().Be(expectedPhotos[5].Title);
            paginatedResult.Items[0].AlbumId.Should().Be(expectedPhotos[5].AlbumId);
        }

        [Test]
        public async Task ShouldGetPhotosFilteredById()
        {
            // Arrange
            var expectedPhotos = fixture.CreateMany<Photo>(10).ToList();
            expectedPhotos[3].Id = 333;

            SetUpPhotos(expectedPhotos);

            // Act 
            var result = await HttpClient.GetAsync($"{PhotosRequestUri}?id=333");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Photo>>();

            paginatedResult.TotalCount.Should().Be(1);
            paginatedResult.TotalPages.Should().Be(1);

            paginatedResult.Items[0].Id.Should().Be(expectedPhotos[3].Id);
            paginatedResult.Items[0].Title.Should().Be(expectedPhotos[3].Title);
            paginatedResult.Items[0].AlbumId.Should().Be(expectedPhotos[3].AlbumId);
        }

        [Test]
        public async Task ShouldReturnBadRequestOnInvalidData()
        {
            // Act 
            var result = await HttpClient.GetAsync($"{PhotosRequestUri}?id=0&albumId=0&title=test");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
