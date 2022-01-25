namespace OpsManagement.IntegrationTests.FeatureTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using OpsManagement.Domain.Currencys.Features;
using static TestFixture;
using OpsManagement.Exceptions;

public class AddCurrencyCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_currency_to_db()
    {
        // Arrange
        var fakeCurrencyOne = new FakeCurrencyForCreationDto { }.Generate();

        // Act
        var command = new AddCurrency.AddCurrencyCommand(fakeCurrencyOne);
        var currencyReturned = await SendAsync(command);
        var currencyCreated = await ExecuteDbContextAsync(db => db.Currencys.SingleOrDefaultAsync());

        // Assert
        currencyReturned.Should().BeEquivalentTo(fakeCurrencyOne, options =>
            options.ExcludingMissingMembers());
        currencyCreated.Should().BeEquivalentTo(fakeCurrencyOne, options =>
            options.ExcludingMissingMembers());
    }
}