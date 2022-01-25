namespace OpsManagement.FunctionalTests.FunctionalTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetCountryTests : TestBase
{
    [Test]
    public async Task get_country_returns_success_when_entity_exists_using_valid_auth_credentials()
    {
        // Arrange
        var fakeCountry = new FakeCountry { }.Generate();

            _client.AddAuth(new[] {"countries.readonly"});
            
        await InsertAsync(fakeCountry);

        // Act
        var route = ApiRoutes.Countrys.GetRecord.Replace(ApiRoutes.Countrys.Id, fakeCountry.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
            
    [Test]
    public async Task get_country_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakeCountry = new FakeCountry { }.Generate();

        await InsertAsync(fakeCountry);

        // Act
        var route = ApiRoutes.Countrys.GetRecord.Replace(ApiRoutes.Countrys.Id, fakeCountry.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task get_country_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakeCountry = new FakeCountry { }.Generate();
        _client.AddAuth();

        await InsertAsync(fakeCountry);

        // Act
        var route = ApiRoutes.Countrys.GetRecord.Replace(ApiRoutes.Countrys.Id, fakeCountry.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}