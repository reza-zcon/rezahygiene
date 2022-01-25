namespace OpsManagement.IntegrationTests.FeatureTests.Currency;

using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using OpsManagement.Domain.Currencys.Features;
using static TestFixture;

public class DeleteCurrencyCommandTests : TestBase
{
    [Test]
    public async Task can_delete_currency_from_db()
    {
        // Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        await InsertAsync(fakeCurrencyOne);
        var currency = await ExecuteDbContextAsync(db => db.Currencys.SingleOrDefaultAsync());
        var id = currency.Id;

        // Act
        var command = new DeleteCurrency.DeleteCurrencyCommand(id);
        await SendAsync(command);
        var currencyResponse = await ExecuteDbContextAsync(db => db.Currencys.ToListAsync());

        // Assert
        currencyResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_currency_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteCurrency.DeleteCurrencyCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}