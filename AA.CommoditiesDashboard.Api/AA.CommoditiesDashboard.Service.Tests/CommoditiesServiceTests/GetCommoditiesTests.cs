using AA.CommoditiesDashboard.Service.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AA.CommoditiesDashboard.Service.Tests.CommoditiesServiceTests;

[TestClass]
public class GetCommoditiesTests : CommoditiesServiceTestsBase
{
    [TestMethod]
    public async Task GivenCallToGetCommodities_ThenReturnCommodities()
    {
        //act
        var result = await Target.GetCommodities();

        //assert
        result.Should().BeEquivalentTo(GetExpectedCommodities());
    }

    private IEnumerable<CommodityDto> GetExpectedCommodities()
    {
        return new List<CommodityDto>
        {
            new () { Id = 1, Name = "Oil" },
            new () { Id = 2, Name = "Gold" }
        };
    }
}

