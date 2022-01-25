namespace OpsManagement.FunctionalTests.FunctionalTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateCountryTests : TestBase
{
    [Test]
    public async Task create_country_returns_created_using_valid_dto_and_valid_auth_credentials()
    {
        // Arrange
        var fakeCountry = new FakeCountryForCreationDto { }.Generate();

            _client.AddAuth(new[] {"countries.readonly"});

        // Act
        var route = ApiRoutes.Countrys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCountry);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
            
    [Test]
    public async Task create_country_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakeCountry = new FakeCountry { }.Generate();

        await InsertAsync(fakeCountry);

        // Act
        var route = ApiRoutes.Countrys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCountry);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task create_country_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakeCountry = new FakeCountry { }.Generate();
        _client.AddAuth();

        await InsertAsync(fakeCountry);

        // Act
        var route = ApiRoutes.Countrys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCountry);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}