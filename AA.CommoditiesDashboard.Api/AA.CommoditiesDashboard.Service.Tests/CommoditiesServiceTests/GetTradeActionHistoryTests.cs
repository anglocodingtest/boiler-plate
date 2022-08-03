using AA.CommoditiesDashboard.Service.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AA.CommoditiesDashboard.Service.Tests.CommoditiesServiceTests;

[TestClass]
public class GetTradeActionHistoryTests : CommoditiesServiceTestsBase
{
    [TestMethod]
    public async Task GivenCallToGetTradeActionHistory_ThenReturn()
    {
        //act
        var result = await Target.GetTradeActionHistory();

        //assert
        result.Should().BeEquivalentTo(GetExpectedTradeActionHistory());
    }

    private IEnumerable<TradeActionHistoryDto> GetExpectedTradeActionHistory()
    {
        return new List<TradeActionHistoryDto>
        {
            new()
            {
                 CommodityName = "Oil",
                ModelName = "S&P",
                Date = new DateTime(2021, 4, 2),
                TradeAction = 3
            },
            new()
            {
                CommodityName = "Oil",
                ModelName = "S&P",
                Date = new DateTime(2021, 3, 25),
                TradeAction = -3
            },
            new()
            {
                CommodityName = "Oil",
                ModelName = "FTSE",
                Date = new DateTime(2022, 7, 15),
                TradeAction = -1
            },
            new()
            {
                CommodityName = "Oil",
                ModelName = "FTSE",
                Date = new DateTime(2022, 7, 1),
                TradeAction = -2
            },
            new()
            {
                CommodityName = "Oil",
                ModelName = "FTSE",
                Date = new DateTime(2022, 6, 8),
                TradeAction = 3
            },
            new()
            {
                CommodityName = "Oil",
                ModelName = "FTSE",
                Date = new DateTime(2022, 6, 3),
                TradeAction = 4
            },
            new()
            {
                CommodityName = "Oil",
                ModelName = "FTSE",
                Date = new DateTime(2022, 5, 7),
                TradeAction = -2
            },
            new()
            {
                CommodityName = "Gold",
                ModelName = "FTSE",
                Date = new DateTime(2022, 3, 3),
                TradeAction = -2
            },
            new()
            {
                CommodityName = "Gold",
                ModelName = "FTSE",
                Date = new DateTime(2022, 2, 2),
                TradeAction = -4
            },
            new()
            {
                CommodityName = "Gold",
                ModelName = "FTSE",
                Date = new DateTime(2021, 12, 2),
                TradeAction = 5
            }
        };
    }
}