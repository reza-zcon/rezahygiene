namespace OpsManagement.FunctionalTests.FunctionalTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetCurrencyListTests : TestBase
{
    [Test]
    public async Task get_currency_list_returns_success_using_valid_auth_credentials()
    {
        // Arrange
        _client.AddAuth(new[] {"currencies.readonly"});

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Currencys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
            
    [Test]
    public async Task get_currency_list_returns_unauthorized_without_valid_token()
    {
        // Arrange
        // N/A

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Currencys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task get_currency_list_returns_forbidden_without_proper_scope()
    {
        // Arrange
        _client.AddAuth();

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Currencys.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}