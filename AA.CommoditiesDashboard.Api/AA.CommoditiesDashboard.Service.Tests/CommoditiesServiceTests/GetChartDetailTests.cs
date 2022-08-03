using AA.CommoditiesDashboard.Service.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AA.CommoditiesDashboard.Service.Tests.CommoditiesServiceTests;

[TestClass] 
public class GetChartDetailTests : CommoditiesServiceTestsBase
{
    [TestMethod]
    public async Task GivenCallToGetChartDetail_ThenReturnChartDetail()
    {
        //arrange
        var commodityId = 1;
        var modelId = 1;        

        //act
        var result = await Target.GetChartDetail(commodityId, modelId);

        //assert
        result.Should().BeEquivalentTo(GetExpectedChartDetail());
    }

    private IEnumerable<ChartDto> GetExpectedChartDetail()
    {
        return new List<ChartDto>
        {
            new () { Date = new DateTime(2021, 3, 25), PnlDaily = 1000.00m, Position = 2 },
            new () { Date = new DateTime(2021, 4, 2), PnlDaily = 1200.00m, Position = 5 }
        };
    }
}

