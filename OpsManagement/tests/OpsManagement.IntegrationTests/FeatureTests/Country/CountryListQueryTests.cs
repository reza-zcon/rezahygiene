namespace OpsManagement.IntegrationTests.FeatureTests.Country;

using OpsManagement.Dtos.Country;
using OpsManagement.SharedTestHelpers.Fakes.Country;
using OpsManagement.Exceptions;
using OpsManagement.Domain.Countrys.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class CountryListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_country_list()
    {
        // Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        var queryParameters = new CountryParametersDto();

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        // Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_country_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        var fakeCountryThree = new FakeCountry { }.Generate();
        var queryParameters = new CountryParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeCountryOne, fakeCountryTwo, fakeCountryThree);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_country_by_Name_in_asc_order()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        fakeCountryOne.Name = "bravo";
        fakeCountryTwo.Name = "alpha";
        var queryParameters = new CountryParametersDto() { SortOrder = "Name" };

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryTwo, options =>
                options.ExcludingMissingMembers());
        countrys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_country_by_Name_in_desc_order()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        fakeCountryOne.Name = "alpha";
        fakeCountryTwo.Name = "bravo";
        var queryParameters = new CountryParametersDto() { SortOrder = "-Name" };

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryTwo, options =>
                options.ExcludingMissingMembers());
        countrys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_country_by_Code_in_asc_order()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        fakeCountryOne.Code = "bravo";
        fakeCountryTwo.Code = "alpha";
        var queryParameters = new CountryParametersDto() { SortOrder = "Code" };

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryTwo, options =>
                options.ExcludingMissingMembers());
        countrys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_country_by_Code_in_desc_order()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        fakeCountryOne.Code = "alpha";
        fakeCountryTwo.Code = "bravo";
        var queryParameters = new CountryParametersDto() { SortOrder = "-Code" };

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryTwo, options =>
                options.ExcludingMissingMembers());
        countrys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_country_list_using_Name()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        fakeCountryOne.Name = "alpha";
        fakeCountryTwo.Name = "bravo";
        var queryParameters = new CountryParametersDto() { Filters = $"Name == {fakeCountryTwo.Name}" };

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys.Should().HaveCount(1);
        countrys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_country_list_using_Code()
    {
        //Arrange
        var fakeCountryOne = new FakeCountry { }.Generate();
        var fakeCountryTwo = new FakeCountry { }.Generate();
        fakeCountryOne.Code = "alpha";
        fakeCountryTwo.Code = "bravo";
        var queryParameters = new CountryParametersDto() { Filters = $"Code == {fakeCountryTwo.Code}" };

        await InsertAsync(fakeCountryOne, fakeCountryTwo);

        //Act
        var query = new GetCountryList.CountryListQuery(queryParameters);
        var countrys = await SendAsync(query);

        // Assert
        countrys.Should().HaveCount(1);
        countrys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCountryTwo, options =>
                options.ExcludingMissingMembers());
    }

}