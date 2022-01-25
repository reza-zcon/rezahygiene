namespace OpsManagement.FunctionalTests.FunctionalTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetCurrencyTests : TestBase
{
    [Test]
    public async Task get_currency_returns_success_when_entity_exists_using_valid_auth_credentials()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();

            _client.AddAuth(new[] {"currencies.readonly"});
            
        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.GetRecord.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
            
    [Test]
    public async Task get_currency_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();

        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.GetRecord.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task get_currency_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();
        _client.AddAuth();

        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.GetRecord.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}