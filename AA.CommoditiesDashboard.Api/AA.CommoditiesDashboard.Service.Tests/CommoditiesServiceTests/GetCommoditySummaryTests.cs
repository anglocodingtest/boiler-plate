using AA.CommoditiesDashboard.Service.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AA.CommoditiesDashboard.Service.Tests.CommoditiesServiceTests;

[TestClass]
public class GetCommoditySummaryTests : CommoditiesServiceTestsBase
{
    [TestMethod]
    public async Task GivenCallToGetTradeActionHistory_ThenReturnCommoditySummary()
    {
        //act
        var result = await Target.GetCommoditySummary();

        //assert
        result.Should().BeEquivalentTo(GetExpectedCommoditySummary());
    }

    private IEnumerable<CommoditySummaryDto> GetExpectedCommoditySummary()
    {
        return new List<CommoditySummaryDto>
        {
            new()
            {
                CommodityId = 1, CommodityName = "Oil",
                ModelId = 1, ModelName = "S&P",
                Date = new DateTime(2021, 4, 2),
                PnlCurrent = 1200m, PnlLtd = 2200.00m,
                Position = 5, Price = 110.00m,
                YearSummaries = new List<YearSummaryDto>
                {
                    new()
                    {
                        DrawdownYtd = 00.00m,
                        PnlYtd = 2200.00m,
                        Year = 2021
                    }
                }
            },
            new()
            {
                CommodityId = 1, CommodityName = "Oil",
                ModelId = 2, ModelName = "FTSE",
                Date = new DateTime(2022, 7, 15),
                PnlCurrent = 1600m, PnlLtd = 15500.00m,
                Position = 1, Price = 200.00m,
                YearSummaries = new List<YearSummaryDto>
                {
                    new()
                    {
                        DrawdownYtd = -10400.00m,
                        PnlYtd = 15500m,
                        Year = 2022
                    }
                }
            },
            new()
            {
                CommodityId = 2, CommodityName = "Gold",
                ModelId = 2, ModelName = "FTSE",
                Date = new DateTime(2022, 3, 3),
                PnlCurrent = -1300m, PnlLtd = 3900.00m,
                Position = 0, Price = 200.00m,
                YearSummaries = new List<YearSummaryDto>
                {
                    new()
                    {
                        DrawdownYtd = 0.00m,
                        PnlYtd = 2400.00m,
                        Year = 2021
                    },
                    new()
                    {
                        DrawdownYtd = -4100.00m,
                        PnlYtd = 1500.00m,
                        Year = 2022
                    }
                }
            }
        };
    }
}