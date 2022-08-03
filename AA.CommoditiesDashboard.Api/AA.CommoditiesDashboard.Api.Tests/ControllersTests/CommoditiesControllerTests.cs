using AA.CommoditiesDashboard.Api.Controllers;
using AA.CommoditiesDashboard.Service;
using AA.CommoditiesDashboard.Service.Dtos;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AA.CommoditiesDashboard.Api.Tests.ControllersTests;

[TestClass]
public class CommoditiesControllerTests
{
    private CommoditiesController _target;
    private Mock<ICommoditiesService> _commoditiesService;
    
    [TestInitialize]
    public void Setup()
    {
        _commoditiesService = new Mock<ICommoditiesService>(MockBehavior.Strict);
        _target = new CommoditiesController(_commoditiesService.Object);
    }

    [TestMethod]
    public async Task GivenCallToGet_ThenReturnsCommodities()
    {
        //Arrange
        var expected = new List<CommodityDto>
        {
            new () { Id = 1, Name = "Oil" },
            new () { Id = 2, Name = "Gold" }
        };
        _commoditiesService.Setup(a => a.GetCommodities()).ReturnsAsync(expected);

        //Act
        var result = await _target.Get();

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task GivenCallToGetModels_ThenReturnsModels()
    {
        //Arrange
        var expected = new List<ModelDto>
        {
            new () { Id = 1, Name = "S&P" },
            new () { Id = 2, Name = "FTSE" }
        };
        _commoditiesService.Setup(a => a.GetModels()).ReturnsAsync(expected);

        //Act
        var result = await _target.GetModels();

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task GivenCallToGetCommoditySummary_ThenReturnsCommoditySummary()
    {
        //Arrange
        var expected = new List<CommoditySummaryDto>
        {
            new()
                {
                    CommodityId = 1, CommodityName = "Oil",
                    ModelId = 1, ModelName = "FTSE",
                    Date = DateTime.Today.AddDays(-5),
                    PnlCurrent = 100.00m, PnlLtd = 200.00m,
                    Position = 3, Price = 90.00m,
                    YearSummaries = new List<YearSummaryDto>
                    {
                        new()
                            {
                                DrawdownYtd = 110.00m,
                                PnlYtd = 50.00m,
                                Year = 2022
                            }
                    }
                },
            new()
                {
                    CommodityId = 1, CommodityName = "Oil",
                    ModelId = 1, ModelName = "Global",
                    Date =  DateTime.Today.AddDays(-5),
                    PnlCurrent = 111.00m, PnlLtd = 201.00m,
                    Position = 2, Price = 90.00m,
                    YearSummaries = new List<YearSummaryDto>
                    {
                        new()
                            {
                                DrawdownYtd = 115.00m,
                                PnlYtd = 55.00m,
                                Year = 2022
                            }
                    }
                }
        };
        _commoditiesService.Setup(a => a.GetCommoditySummary()).ReturnsAsync(expected);

        //Act
        var result = await _target.GetCommoditySummary();
       
        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task GivenCallToGetTradeActions_ThenReturnTradeActions()
    {
        //Arrange
        var expected = new List<TradeActionHistoryDto>
        {
            new()
                {
                   CommodityName = "Oil",
                   ModelName = "FTSE",
                    Date =  DateTime.Today.AddDays(-5),
                    TradeAction = 5
                },
            new()
                {
                   CommodityName = "Oil",
                   ModelName = "Global",
                    Date =  DateTime.Today.AddDays(-5),
                    TradeAction = 3
                }
        };
        _commoditiesService.Setup(a => a.GetTradeActionHistory()).ReturnsAsync(expected);

        //Act
        var result = await _target.GetTradeActions();

        //Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public async Task GivenCallToGetGetChartDetail_ThenReturnGetChartDetail()
    {
        //Arrange
        var commodityId = 1;
        var modelId = 1;
        var expected = new List<ChartDto>
        {
            new () { Date = new DateTime(2021, 3, 25), PnlDaily = 1000.00m, Position = 2 },
            new () { Date = new DateTime(2021, 4, 2), PnlDaily = 1200.00m, Position = 5 },
            new () { Date = new DateTime(2021, 7, 6), PnlDaily = 1310.00m, Position = -3 },
        };
        _commoditiesService.Setup(a => a.GetChartDetail(commodityId, modelId)).ReturnsAsync(expected);

        //Act
        var result = await _target.GetChartDetail(commodityId, modelId);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
}

