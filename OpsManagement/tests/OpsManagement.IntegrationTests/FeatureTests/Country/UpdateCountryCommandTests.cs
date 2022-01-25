namespace OpsManagement.IntegrationTests.FeatureTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.IntegrationTests.TestUtilities;
using OpsManagement.Dtos.Country;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using OpsManagement.Domain.Countrys.Features;
using static TestFixture;

public class UpdateCountryCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_country_in_db()
    {
        // Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var updatedCountryDto = new FakeCountryForUpdateDto { }.Generate();
        await InsertAsync(fakeCountryOne);

        var country = await ExecuteDbContextAsync(db => db.Countrys.SingleOrDefaultAsync());
        var id = country.Id;

        // Act
        var command = new UpdateCountry.UpdateCountryCommand(id, updatedCountryDto);
        await SendAsync(command);
        var updatedCountry = await ExecuteDbContextAsync(db => db.Countrys.Where(c => c.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedCountry.Should().BeEquivalentTo(updatedCountryDto, options =>
            options.ExcludingMissingMembers());
    }
}