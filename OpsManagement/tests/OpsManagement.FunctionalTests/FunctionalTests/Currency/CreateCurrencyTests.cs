namespace OpsManagement.FunctionalTests.FunctionalTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateCurrencyTests : TestBase
{
    [Test]
    public async Task create_currency_returns_created_using_valid_dto_and_valid_auth_credentials()
    {
        // Arrange
        var fakeCurrency = new FakeCurrencyForCreationDto { }.Generate();

            _client.AddAuth(new[] {"currencies.readonly"});

        // Act
        var route = ApiRoutes.Currencys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCurrency);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
            
    [Test]
    public async Task create_currency_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();

        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCurrency);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task create_currency_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();
        _client.AddAuth();

        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeCurrency);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}