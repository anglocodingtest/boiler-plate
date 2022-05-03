using AA.CommoditiesDashboard.Api.Database;
using AA.CommoditiesDashboard.Api.Entities;
using AA.CommoditiesDashboard.Api.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AA.CommoditiesDashboard.Api.Tests
{
    public class CommoditiesServiceTests
    {
        protected class AnalyticsSeedDataFixture : AnalyticsDataFixture
        {
            public AnalyticsSeedDataFixture() : base()
            {
                var modelCommodity = new ModelCommodity
                {
                    Commodity = new Commodity
                    {
                        Name = "Commodity 1"
                    },
                    Model = new Model
                    {
                        Name = "Model 1"
                    },
                    VarAllocation = 999,
                    DailyMetrics = new List<DailyMetrics>
                    {
                        new DailyMetrics
                        {
                            Date = DateTime.Now,
                            Contract = "MAR 21",
                            Price = 1000,
                            Position = 1,
                            NewTradeAction = 10,
                            PnlDaily = 10000,
                        },
                        new DailyMetrics
                        {
                            Date = DateTime.Now.AddDays(-10),
                            Contract = "MAR 21",
                            Price = 2000,
                            Position = 2,
                            NewTradeAction = 20,
                            PnlDaily = 20000,
                        }
                    }
                };
                AnalyticsContext.Add(modelCommodity);
                AnalyticsContext.SaveChanges();
            }
        }

        protected class AnalyticsDataFixture : System.IDisposable
        {
            public AnalyticsDbContext AnalyticsContext { get; private set; } = new AnalyticsDbContext(new DbContextOptionsBuilder<AnalyticsDbContext>()
                        .UseInMemoryDatabase(databaseName: "AnalyticsDb")
                        .Options);

            public AnalyticsDataFixture()
            {
            }

            public void Dispose()
            {
                AnalyticsContext.Dispose();
            }
        }

        [Fact]
        public void Constructor_WhenContextIsNull_ThrowsArgumentNullException()
        {
            // Arrange/Act
            Action act = () => new CommoditiesService(null);

            // Assert
            act.Should()
                .Throw<Exception>("constructor should fail when passing null for parameter: {0}", "Context");
        }

        [Fact]
        public void Constructor_WhenContextIsNotNull_CreatesService()
        {
            // Arrange
            using var context = new AnalyticsDataFixture().AnalyticsContext;

            //Act
            var sut = new CommoditiesService(context);
            
            // Assert
            sut.Should().NotBeNull();
        }

        [Fact]
        public async Task GetModelCommodities_ReturnsExpectedModel()
        {
            using var context = new AnalyticsSeedDataFixture().AnalyticsContext;
            var sut = new CommoditiesService(context);

            var items = await sut.GetModelCommodities();

            items.Count().Should().Be(1);

            var first = items.FirstOrDefault();

            first.ModelName.Should().Be("Model 1");
            first.CommodityName.Should().Be("Commodity 1");
            first.Position.Should().Be(1);
            first.VarAllocation.Should().Be(999);
            first.PnLDaily.Should().Be(10000);
            first.Price.Should().Be(1000);
            first.PnlLTD.Should().Be(30000);
        }
    }
}