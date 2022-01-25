namespace OpsManagement.FunctionalTests.FunctionalTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateCurrencyRecordTests : TestBase
{
    [Test]
    public async Task put_currency_returns_nocontent_when_entity_exists_and_auth_credentials_are_valid()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();
        var updatedCurrencyDto = new FakeCurrencyForUpdateDto { }.Generate();

            _client.AddAuth(new[] {"currencies.readonly"});
            
        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.Put.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedCurrencyDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
            
    [Test]
    public async Task put_currency_returns_unauthorized_without_valid_token()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();
        var updatedCurrencyDto = new FakeCurrencyForUpdateDto { }.Generate();

        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.Put.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedCurrencyDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task put_currency_returns_forbidden_without_proper_scope()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();
        var updatedCurrencyDto = new FakeCurrencyForUpdateDto { }.Generate();
        _client.AddAuth();

        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.Put.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedCurrencyDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}