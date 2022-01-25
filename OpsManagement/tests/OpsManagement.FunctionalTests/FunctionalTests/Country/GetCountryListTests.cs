namespace OpsManagement.FunctionalTests.FunctionalTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetCountryListTests : TestBase
{
    [Test]
    public async Task get_country_list_returns_success_using_valid_auth_credentials()
    {
        // Arrange
        _client.AddAuth(new[] {"countries.readonly"});

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Countrys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
            
    [Test]
    public async Task get_country_list_returns_unauthorized_without_valid_token()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Countrys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task get_country_list_returns_forbidden_without_proper_scope()
    {
        // Arrange
        _client.AddAuth();

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Countrys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}