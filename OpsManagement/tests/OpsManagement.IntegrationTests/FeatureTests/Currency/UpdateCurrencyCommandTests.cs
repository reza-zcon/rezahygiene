namespace OpsManagement.IntegrationTests.FeatureTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.IntegrationTests.TestUtilities;
using OpsManagement.Dtos.Currency;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using OpsManagement.Domain.Currencys.Features;
using static TestFixture;

public class UpdateCurrencyCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_currency_in_db()
    {
        // Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var updatedCurrencyDto = new FakeCurrencyForUpdateDto { }.Generate();
        await InsertAsync(fakeCurrencyOne);

        var currency = await ExecuteDbContextAsync(db => db.Currencys.SingleOrDefaultAsync());
        var id = currency.Id;

        // Act
        var command = new UpdateCurrency.UpdateCurrencyCommand(id, updatedCurrencyDto);
        await SendAsync(command);
        var updatedCurrency = await ExecuteDbContextAsync(db => db.Currencys.Where(c => c.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedCurrency.Should().BeEquivalentTo(updatedCurrencyDto, options =>
            options.ExcludingMissingMembers());
    }
}