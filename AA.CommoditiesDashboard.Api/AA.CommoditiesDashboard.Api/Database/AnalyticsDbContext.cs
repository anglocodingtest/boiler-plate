using AA.CommoditiesDashboard.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Api.Database
{
    public class AnalyticsDbContext : DbContext
    {
        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options)
            : base(options) 
        {
        }

        public DbSet<DailyMetrics> DailyMetrics { get; set; }
        public DbSet<ModelCommodity> ModelCommodities { get; set; }
    }
}
