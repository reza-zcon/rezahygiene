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

public class CurrencyQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_currency_with_accurate_props()
    {
        // Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        await InsertAsync(fakeCurrencyOne);

        // Act
        var query = new GetCurrency.CurrencyQuery(fakeCurrencyOne.Id);
        var currencys = await SendAsync(query);

        // Assert
        currencys.Should().BeEquivalentTo(fakeCurrencyOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_currency_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetCurrency.CurrencyQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}