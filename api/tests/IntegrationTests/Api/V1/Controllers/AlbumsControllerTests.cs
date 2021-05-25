using Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using System.Linq;
using AutoFixture;
using Application.Common.Models;

namespace IntegrationTests.Api.V1.Controllers
{
    using static TestsSetup;

    public class AlbumsControllerTests
    {
        private const string AlbumsRequestUri = "api/v1/albums";

        private Fixture fixture = new Fixture();

        [Test]
        public async Task ShouldGetAllAlbums()
        {
            // Arrange
            var expectedAlbums = fixture.CreateMany<Album>(10).ToList();
            SetUpAlbums(expectedAlbums);

            // Act 
            var result = await HttpClient.GetAsync(AlbumsRequestUri);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Album>>();

            paginatedResult.TotalCount.Should().Be(expectedAlbums.Count);
            paginatedResult.TotalPages.Should().Be(1);

            for (int i = 0; i < paginatedResult.Items.Count; i++)
            {
                paginatedResult.Items[i].Id.Should().Be(expectedAlbums[i].Id);
                paginatedResult.Items[i].Title.Should().Be(expectedAlbums[i].Title);
                paginatedResult.Items[i].UserId.Should().Be(expectedAlbums[i].UserId);
            }
        }

        [Test]
        public async Task ShouldGetAlbumsFilteredByUserId()
        {
            // Arrange
            var expectedAlbums = fixture.CreateMany<Album>(10).ToList();
            expectedAlbums[1].UserId = 1;

            SetUpAlbums(expectedAlbums);

            // Act 
            var result = await HttpClient.GetAsync($"{AlbumsRequestUri}?userId=1");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Album>>();

            paginatedResult.TotalCount.Should().Be(1);
            paginatedResult.TotalPages.Should().Be(1);

            paginatedResult.Items[0].Id.Should().Be(expectedAlbums[1].Id);
            paginatedResult.Items[0].Title.Should().Be(expectedAlbums[1].Title);
            paginatedResult.Items[0].UserId.Should().Be(expectedAlbums[1].UserId);
        }

        [Test]
        public async Task ShouldGetAlbumsFilteredByTitle()
        {
            // Arrange
            var expectedAlbums = fixture.CreateMany<Album>(10).ToList();
            expectedAlbums[5].Title = "testing";

            SetUpAlbums(expectedAlbums);

            // Act 
            var result = await HttpClient.GetAsync($"{AlbumsRequestUri}?title=testing");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Album>>();

            paginatedResult.TotalCount.Should().Be(1);
            paginatedResult.TotalPages.Should().Be(1);

            paginatedResult.Items[0].Id.Should().Be(expectedAlbums[5].Id);
            paginatedResult.Items[0].Title.Should().Be(expectedAlbums[5].Title);
            paginatedResult.Items[0].UserId.Should().Be(expectedAlbums[5].UserId);
        }

        [Test]
        public async Task ShouldGetAlbumsFilteredById()
        {
            // Arrange
            var expectedAlbums = fixture.CreateMany<Album>(10).ToList();
            expectedAlbums[3].Id = 333;

            SetUpAlbums(expectedAlbums);

            // Act 
            var result = await HttpClient.GetAsync($"{AlbumsRequestUri}?id=333");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var paginatedResult = await result.Content.ReadFromJsonAsync<PaginatedListTest<Album>>();

            paginatedResult.TotalCount.Should().Be(1);
            paginatedResult.TotalPages.Should().Be(1);

            paginatedResult.Items[0].Id.Should().Be(expectedAlbums[3].Id);
            paginatedResult.Items[0].Title.Should().Be(expectedAlbums[3].Title);
            paginatedResult.Items[0].UserId.Should().Be(expectedAlbums[3].UserId);
        }

        [Test]
        public async Task ShouldReturnBadRequestOnInvalidData()
        {
            // Act 
            var result = await HttpClient.GetAsync($"{AlbumsRequestUri}?id=0&userId=0&title=test");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
