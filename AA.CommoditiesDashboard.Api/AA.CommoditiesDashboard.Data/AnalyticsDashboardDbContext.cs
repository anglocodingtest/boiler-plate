using AA.CommoditiesDashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Data;

public class AnalyticsDashboardDbContext : DbContext
{
    public AnalyticsDashboardDbContext(DbContextOptions<AnalyticsDashboardDbContext> options)
        : base(options)
    {
    }

    public DbSet<CommodityData> CommodityData { get; set; }
    public DbSet<Commodity> Commodities { get; set; }
    public DbSet<Model> Models { get; set; }
}