namespace OpsManagement.IntegrationTests.FeatureTests.Country;

using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using OpsManagement.Domain.Countrys.Features;
using static TestFixture;

public class DeleteCountryCommandTests : TestBase
{
    [Test]
    public async Task can_delete_country_from_db()
    {
        // Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        await InsertAsync(fakeCountryOne);
        var country = await ExecuteDbContextAsync(db => db.Countrys.SingleOrDefaultAsync());
        var id = country.Id;

        // Act
        var command = new DeleteCountry.DeleteCountryCommand(id);
        await SendAsync(command);
        var countryResponse = await ExecuteDbContextAsync(db => db.Countrys.ToListAsync());

        // Assert
        countryResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_country_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteCountry.DeleteCountryCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}