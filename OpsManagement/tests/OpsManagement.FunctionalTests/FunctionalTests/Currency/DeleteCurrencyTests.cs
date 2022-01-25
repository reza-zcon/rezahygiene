namespace OpsManagement.FunctionalTests.FunctionalTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteCurrencyTests : TestBase
{
    [Test]
    public async Task delete_currency_returns_nocontent_when_entity_exists_and_auth_credentials_are_valid()
    {
        // Arrange
        var fakeCurrency = new FakeCurrency { }.Generate();

            _client.AddAuth(new[] {"currencies.fullaccess"});
            
        await InsertAsync(fakeCurrency);

        // Act
        var route = ApiRoutes.Currencys.Delete.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
            
        [Test]
        public async Task delete_currency_returns_unauthorized_without_valid_token()
        {
            // Arrange
            var fakeCurrency = new FakeCurrency { }.Generate();

            await InsertAsync(fakeCurrency);

            // Act
            var route = ApiRoutes.Currencys.Delete.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
            
        [Test]
        public async Task delete_currency_returns_forbidden_without_proper_scope()
        {
            // Arrange
            var fakeCurrency = new FakeCurrency { }.Generate();
            _client.AddAuth();

            await InsertAsync(fakeCurrency);

            // Act
            var route = ApiRoutes.Currencys.Delete.Replace(ApiRoutes.Currencys.Id, fakeCurrency.Id.ToString());
            var result = await _client.DeleteRequestAsync(route);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
}