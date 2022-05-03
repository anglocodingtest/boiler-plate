using AA.CommoditiesDashboard.Api.Database;
using AA.CommoditiesDashboard.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Api.Services
{
    // TODO: Add Automapper to map Entities to Domain
    public class CommoditiesService : ICommoditiesService
    {
        private readonly AnalyticsDbContext _context;

        public CommoditiesService(AnalyticsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ChartPoint>> GetChartData(long id)
            => await _context.DailyMetrics
                .Where(x => x.ModelCommodity.Id == id)
                .Select(x => new ChartPoint 
                {
                    Date = x.Date,
                    Pnl = x.PnlDaily
                })
                .ToListAsync();

        public async Task<IEnumerable<ModelCommodity>> GetModelCommodities()
            => await _context.ModelCommodities
            .Select(t => t.Id).Distinct()
            .SelectMany(key => _context.ModelCommodities
                .Include(x => x.Model)
                .Include(x => x.Commodity)
                .Include(x => x.DailyMetrics)
                .Where(t => t.Id == key)
                )
                .Select(
                x => new ModelCommodity
                {
                    Id = x.Id,
                    CommodityName = x.Commodity.Name,
                    ModelName = x.Model.Name,
                    VarAllocation = x.VarAllocation,
                    PnLDaily = x.DailyMetrics.OrderByDescending(m => m.Date).First().PnlDaily,
                    Price = x.DailyMetrics.OrderByDescending(m => m.Date).First().Price,
                    Position = x.DailyMetrics.OrderByDescending(m => m.Date).First().Position,
                    PnlLTD = x.DailyMetrics.Sum(m => m.PnlDaily)
                }).ToListAsync();

        public async Task<IEnumerable<TradeAction>> GetTradeActions()
           => await _context.DailyMetrics
            .Select(t => t.ModelCommodity.Id).Distinct()
            .SelectMany(key => _context.DailyMetrics
                .Include(x => x.ModelCommodity)
                    .ThenInclude(x => x.Model)
                .Include(x => x.ModelCommodity)
                    .ThenInclude(x => x.Commodity)
                .Where(t => t.ModelCommodity.Id == key)
                .OrderByDescending(t => t.Date).Take(5)
            ).Select(
                x => new TradeAction
                {
                    Id = x.Id,
                    ModelName = x.ModelCommodity.Model.Name,
                    CommodityName = x.ModelCommodity.Commodity.Name,
                    NewTradeAction = x.NewTradeAction
                }).ToListAsync();
    }
}
