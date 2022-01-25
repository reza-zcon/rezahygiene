namespace OpsManagement.FunctionalTests.FunctionalTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteCountryTests : TestBase
{
    [Test]
    public async Task delete_country_returns_nocontent_when_entity_exists_and_auth_credentials_are_valid()
    {
        // Arrange
        var fakeCountry = new FakeCountry { }.Generate();

            _client.AddAuth(new[] {"countries.fullaccess"});
            
        await InsertAsync(fakeCountry);

        // Act
        var route = ApiRoutes.Countrys.Delete.Replace(ApiRoutes.Countrys.Id, fakeCountry.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
            
        [Test]
        public async Task delete_country_returns_unauthorized_without_valid_token()
        {
            // Arrange
            var fakeCountry = new FakeCountry { }.Generate();

            await InsertAsync(fakeCountry);

            // Act
            var route = ApiRoutes.Countrys.Delete.Replace(ApiRoutes.Countrys.Id, fakeCountry.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
            
        [Test]
        public async Task delete_country_returns_forbidden_without_proper_scope()
        {
            // Arrange
            var fakeCountry = new FakeCountry { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeCountry);

            // Act
            var route = ApiRoutes.Countrys.Delete.Replace(ApiRoutes.Countrys.Id, fakeCountry.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
}