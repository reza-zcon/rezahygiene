namespace OpsManagement.IntegrationTests.FeatureTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using OpsManagement.Domain.Countrys.Features;
using static TestFixture;
using OpsManagement.Exceptions;

public class AddCountryCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_country_to_db()
    {
        // Arrange
        var fakeCountryOne = new FakeCountryForCreationDto { }.Generate();

        // Act
        var command = new AddCountry.AddCountryCommand(fakeCountryOne);
        var countryReturned = await SendAsync(command);
        var countryCreated = await ExecuteDbContextAsync(db => db.Countrys.SingleOrDefaultAsync());

        // Assert
        countryReturned.Should().BeEquivalentTo(fakeCountryOne, options =>
            options.ExcludingMissingMembers());
        countryCreated.Should().BeEquivalentTo(fakeCountryOne, options =>
            options.ExcludingMissingMembers());
    }
}