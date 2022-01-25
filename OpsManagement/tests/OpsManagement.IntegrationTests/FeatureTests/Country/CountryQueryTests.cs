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

public class CountryQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_country_with_accurate_props()
    {
        // Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        await InsertAsync(fakeCountryOne);

        // Act
        var query = new GetCountry.CountryQuery(fakeCountryOne.Id);
        var countrys = await SendAsync(query);

        // Assert
        countrys.Should().BeEquivalentTo(fakeCountryOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_country_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetCountry.CountryQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}