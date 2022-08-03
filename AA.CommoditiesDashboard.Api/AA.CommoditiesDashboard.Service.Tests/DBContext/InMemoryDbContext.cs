using AA.CommoditiesDashboard.Data;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Service.Tests.DBContext;

public class InMemoryDbContext : AnalyticsDashboardDbContext
{
    public InMemoryDbContext() : base(new DbContextOptionsBuilder<AnalyticsDashboardDbContext>()
        .UseInMemoryDatabase("AnalyticsDashboard").Options)
    {
    }
}