using AA.CommoditiesDashboard.Data;
using AA.CommoditiesDashboard.Service.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AA.CommoditiesDashboard.Service;
public class CommoditiesService : ICommoditiesService
{
    private readonly AnalyticsDashboardDbContext _dbContext;
    public CommoditiesService(AnalyticsDashboardDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<TradeActionHistoryDto>> GetTradeActionHistory()
    {
        return await (from m in _dbContext.Models
            from c in _dbContext.Commodities
            from cd in
                _dbContext.CommodityData
                    .Include(a => a.Model)
                    .Include(a => a.Commodity)
                    .Where(d => d.Model.Id == m.Id && d.Commodity.Id == c.Id)
                    .OrderByDescending(d => d.Date).Take(5)
            select new TradeActionHistoryDto
            {
                ModelName = m.Name,
                CommodityName = c.Name,
                TradeAction = cd.NewTradeAction,
                Date = cd.Date
            }).ToListAsync();
    }

    public async Task<IEnumerable<CommoditySummaryDto>> GetCommoditySummary()
    {
        var summary = await (from gp in
                from m in _dbContext.Models
                from c in _dbContext.Commodities
                from cd in
                    _dbContext.CommodityData
                        .Include(a => a.Model)
                        .Include(a => a.Commodity)
                        .Where(d => d.Model.Id == m.Id && d.Commodity.Id == c.Id)
                        .OrderByDescending(o => o.Date)
                select new
                {
                    ComodityId = c.Id,
                    ModelId = m.Id,
                    Comodity = cd
                }
            group gp by new { gp.ModelId, gp.ComodityId }
            into cGroup
            select new CommoditySummaryDto
            {
                ModelId = cGroup.Key.ModelId,
                CommodityId = cGroup.Key.ComodityId,
                ModelName = _dbContext.Models.FirstOrDefault(m => m.Id == cGroup.Key.ModelId).Name,
                CommodityName = _dbContext.Commodities.FirstOrDefault(c => c.Id == cGroup.Key.ComodityId).Name,
                PnlCurrent = cGroup.Select(g => g.Comodity).OrderByDescending(o => o.Date).First().PnlDaily,
                Position = cGroup.Select(g => g.Comodity).OrderByDescending(o => o.Date).First().Position,
                Price = cGroup.Select(g => g.Comodity).OrderByDescending(o => o.Date).First().Price,
                PnlLtd = cGroup.Select(g => g.Comodity).Sum(s => s.PnlDaily),
                Date = cGroup.Select(g => g.Comodity).OrderByDescending(o => o.Date).First().Date
            }).ToListAsync();

        var yearSummaryAll = await (from a in _dbContext.CommodityData.Include(c => c.Model).Include(c => c.Commodity)
            group a by new { ModelId = a.Model.Id, CommodityId = a.Commodity.Id, a.Date.Year }
            into cGroup
            select new
            {
                cGroup.Key.ModelId,
                cGroup.Key.CommodityId,
                cGroup.Key.Year,
                PnlYtd = cGroup.Sum(s => s.PnlDaily),
                DrawdownYtd = cGroup.OrderByDescending(o => o.Date).First().PnlDaily - cGroup.Max(s => s.PnlDaily)
            }).ToListAsync();

        summary.ForEach(r => r.YearSummaries = yearSummaryAll
            .Where(s => r.ModelId == s.ModelId && r.CommodityId == s.CommodityId)
            .Select(s => new YearSummaryDto { Year = s.Year, DrawdownYtd = s.DrawdownYtd, PnlYtd = s.PnlYtd }));

        return summary;
    }

    public async Task<IEnumerable<ModelDto>> GetModels() =>
        await (from m in _dbContext.Models
            select new ModelDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToListAsync();

    public async Task<IEnumerable<CommodityDto>> GetCommodities() => 
        await (from m in _dbContext.Commodities
        select new CommodityDto
        {
            Id = m.Id,
            Name = m.Name
        }).ToListAsync();

    public async Task<IEnumerable<ChartDto>> GetChartDetail(long commodityId, long modelId)
    {
        return await (from cd in _dbContext.CommodityData
                    .Include(a => a.Model)
                    .Include(a => a.Commodity)
                where cd.Model.Id == modelId && cd.Commodity.Id == commodityId
                orderby cd.Date 
                select new ChartDto
                {
                    Date = cd.Date,
                    PnlDaily = cd.PnlDaily,
                    Position = cd.Position,
                }
            ).ToListAsync();
    }
}