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

    public class UsersControllerTests
    {
        private const string UsersRequestUri = "api/v1/users";

        private Fixture fixture = new Fixture();

        [Test]
        public async Task ShouldGetAllUsers()
        {
            // Arrange
            var expectedUsers = fixture.CreateMany<User>(10).ToList();
            SetUpUsers(expectedUsers);

            // Act 
            var result = await HttpClient.GetAsync(UsersRequestUri);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultContent = await result.Content.ReadFromJsonAsync<IEnumerable<User>>();
            var actualUsers = resultContent.ToList();

            actualUsers.Count.Should().Be(actualUsers.Count);
            for (int i = 0; i < actualUsers.Count; i++)
            {
                actualUsers[i].Id.Should().Be(expectedUsers[i].Id);
                actualUsers[i].Name.Should().Be(expectedUsers[i].Name);
                actualUsers[i].Phone.Should().Be(expectedUsers[i].Phone);
                actualUsers[i].Username.Should().Be(expectedUsers[i].Username);
                actualUsers[i].Website.Should().Be(expectedUsers[i].Website);
                actualUsers[i].Company.Should().Be(expectedUsers[i].Company);
                actualUsers[i].Address.City.Should().Be(expectedUsers[i].Address.City);
                actualUsers[i].Address.Geo.Should().Be(expectedUsers[i].Address.Geo);
                actualUsers[i].Address.Street.Should().Be(expectedUsers[i].Address.Street);
                actualUsers[i].Address.Suite.Should().Be(expectedUsers[i].Address.Suite);
                actualUsers[i].Address.ZipCode.Should().Be(expectedUsers[i].Address.ZipCode);
            }
        }

        [Test]
        public async Task ShouldGetUsersFilteredByName()
        {
            // Arrange
            var expectedUsers = fixture.CreateMany<User>(10).ToList();
            expectedUsers[1].Name = "test";

            SetUpUsers(expectedUsers);

            // Act 
            var result = await HttpClient.GetAsync($"{UsersRequestUri}?name=test");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultContent = await result.Content.ReadFromJsonAsync<IEnumerable<User>>();
            var actualUsers = resultContent.ToList();

            actualUsers.Count.Should().Be(1);
            actualUsers[0].Id.Should().Be(expectedUsers[1].Id);
        }

        [Test]
        public async Task ShouldGetUsersFilteredById()
        {
            // Arrange
            var expectedUsers = fixture.CreateMany<User>(10).ToList();
            expectedUsers[3].Id = 333;

            SetUpUsers(expectedUsers);

            // Act 
            var result = await HttpClient.GetAsync($"{UsersRequestUri}?userId=333");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultContent = await result.Content.ReadFromJsonAsync<IEnumerable<User>>();
            var actualUsers = resultContent.ToList();

            actualUsers.Count.Should().Be(1);
            actualUsers[0].Id.Should().Be(expectedUsers[3].Id);
        }

        [Test]
        public async Task ShouldReturnBadRequestOnInvalidData()
        {
            // Act 
            var result = await HttpClient.GetAsync($"{UsersRequestUri}?userId=0");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
