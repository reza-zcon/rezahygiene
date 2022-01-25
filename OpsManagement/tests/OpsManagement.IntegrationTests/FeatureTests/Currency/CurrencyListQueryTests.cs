namespace OpsManagement.IntegrationTests.FeatureTests.Currency;

using OpsManagement.Dtos.Currency;
using OpsManagement.SharedTestHelpers.Fakes.Currency;
using OpsManagement.Exceptions;
using OpsManagement.Domain.Currencys.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class CurrencyListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_currency_list()
    {
        // Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        var queryParameters = new CurrencyParametersDto();

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        // Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_currency_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        var fakeCurrencyThree = new FakeCurrency { }.Generate();
        var queryParameters = new CurrencyParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo, fakeCurrencyThree);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_currency_by_Name_in_asc_order()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        fakeCurrencyOne.Name = "bravo";
        fakeCurrencyTwo.Name = "alpha";
        var queryParameters = new CurrencyParametersDto() { SortOrder = "Name" };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyTwo, options =>
                options.ExcludingMissingMembers());
        currencys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_currency_by_Name_in_desc_order()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        fakeCurrencyOne.Name = "alpha";
        fakeCurrencyTwo.Name = "bravo";
        var queryParameters = new CurrencyParametersDto() { SortOrder = "-Name" };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyTwo, options =>
                options.ExcludingMissingMembers());
        currencys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_currency_by_Code_in_asc_order()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        fakeCurrencyOne.Code = "bravo";
        fakeCurrencyTwo.Code = "alpha";
        var queryParameters = new CurrencyParametersDto() { SortOrder = "Code" };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyTwo, options =>
                options.ExcludingMissingMembers());
        currencys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_currency_by_Code_in_desc_order()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        fakeCurrencyOne.Code = "alpha";
        fakeCurrencyTwo.Code = "bravo";
        var queryParameters = new CurrencyParametersDto() { SortOrder = "-Code" };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyTwo, options =>
                options.ExcludingMissingMembers());
        currencys
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_currency_list_using_Name()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        fakeCurrencyOne.Name = "alpha";
        fakeCurrencyTwo.Name = "bravo";
        var queryParameters = new CurrencyParametersDto() { Filters = $"Name == {fakeCurrencyTwo.Name}" };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys.Should().HaveCount(1);
        currencys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_currency_list_using_Code()
    {
        //Arrange
        var fakeCurrencyOne = new FakeCurrency { }.Generate();
        var fakeCurrencyTwo = new FakeCurrency { }.Generate();
        fakeCurrencyOne.Code = "alpha";
        fakeCurrencyTwo.Code = "bravo";
        var queryParameters = new CurrencyParametersDto() { Filters = $"Code == {fakeCurrencyTwo.Code}" };

        await InsertAsync(fakeCurrencyOne, fakeCurrencyTwo);

        //Act
        var query = new GetCurrencyList.CurrencyListQuery(queryParameters);
        var currencys = await SendAsync(query);

        // Assert
        currencys.Should().HaveCount(1);
        currencys
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeCurrencyTwo, options =>
                options.ExcludingMissingMembers());
    }

}