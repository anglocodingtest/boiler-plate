global using AA.CommoditiesDashboard.Data.Entities;
global using AA.CommoditiesDashboard.Service.Tests.DBContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AA.CommoditiesDashboard.Service.Tests.CommoditiesServiceTests;

public class CommoditiesServiceTestsBase
{
    protected CommoditiesService Target;
    private InMemoryDbContext _dbContext;
    
    [TestInitialize]
    public async Task Initialize()
    {
        _dbContext = new InMemoryDbContext();
        Target = new CommoditiesService(_dbContext);

        await SetupData();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    private async Task SetupData()
    {
        var id = 1;

        var commodities = new List<Commodity>
            {
                new() { Id = 1, Name = "Oil" },
                new() { Id = 2, Name = "Gold" }
            };

        var models = new List<Model>
            {
                new() { Id = 1, Name = "S&P" },
                new() { Id = 2, Name = "FTSE" }
            };

        var commoditiesData = new List<CommodityData>
        {
            // First commodity (oil), First model (S&P) - total 2
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.First(), Contract = "March 2021",
                Date = new DateTime(2021, 3, 25),
                NewTradeAction = -3, PnlDaily = 1000m,
                Position = 2, Price = 100m
            },
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.First(), Contract = "April 2021",
                Date = new DateTime(2021, 4, 2),
                NewTradeAction = 3, PnlDaily = 1200m,
                Position = 5, Price = 110m
            },

            // First commodity (oil), Second model (FTSE) - total 6
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.Skip(1).Take(1).Single(), Contract = "May 2022",
                Date = new DateTime(2022, 5, 2),
                NewTradeAction = 0, PnlDaily = 2000m,
                Position = 3, Price = 160m
            },
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.Skip(1).Take(1).Single(), Contract = "May 2022",
                Date = new DateTime(2022, 5, 7),
                NewTradeAction = -2, PnlDaily = -2300m,
                Position = 1, Price = 167m
            },
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.Skip(1).Take(1).Single(), Contract = "June 2022",
                Date = new DateTime(2022, 6, 3),
                NewTradeAction = 4, PnlDaily = 2000m,
                Position = 3, Price = 160m
            },
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.Skip(1).Take(1).Single(), Contract = "June 2022",
                Date = new DateTime(2022, 6, 8),
                NewTradeAction = 3, PnlDaily = 200m,
                Position = 2, Price = 177m
            },
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.Skip(1).Take(1).Single(), Contract = "July 2022",
                Date = new DateTime(2022, 7, 1),
                NewTradeAction = -2, PnlDaily = 12000m,
                Position = 3, Price = 190m
            },
            new()
            {
                Id = id++, Commodity = commodities.First(),
                Model = models.Skip(1).Take(1).Single(), Contract = "July 2022",
                Date = new DateTime(2022, 7, 15),
                NewTradeAction = -1, PnlDaily = 1600m,
                Position = 1, Price = 200m
            },

            // Second commodity (oil), Second model (S&P) - total 3
            new()
            {
                Id = id++, Commodity = commodities.Skip(1).Take(1).Single(),
                Model = models.Skip(1).Take(1).Single(), Contract = "Dec 2021",
                Date = new DateTime(2021, 12, 2),
                NewTradeAction = 5, PnlDaily = 2400m,
                Position = 6, Price = 167m
            },
            new()
            {
                Id = id++, Commodity = commodities.Skip(1).Take(1).Single(),
               Model = models.Skip(1).Take(1).Single(), Contract = "Feb 2022",
                Date = new DateTime(2022, 2, 2),
                NewTradeAction = -4, PnlDaily = 2800m,
                Position = 2, Price = 180m
            },
            new()
            {
                Id = id, Commodity = commodities.Skip(1).Take(1).Single(),
                Model = models.Skip(1).Take(1).Single(), Contract = "March 2022",
                Date = new DateTime(2022, 3, 3),
                NewTradeAction = -2, PnlDaily = -1300m,
                Position = 0, Price = 200m
            }
        };

        await _dbContext.Commodities.AddRangeAsync(commodities);
        await _dbContext.Models.AddRangeAsync(models);
        await _dbContext.CommodityData.AddRangeAsync(commoditiesData);
        await _dbContext.SaveChangesAsync();
    }  
}

