namespace OpsManagement.IntegrationTests;

using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions.Extensions;
using static TestFixture;

public class TestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}