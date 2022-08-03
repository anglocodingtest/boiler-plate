using AA.CommoditiesDashboard.Service.Dtos;

namespace AA.CommoditiesDashboard.Service;

public interface ICommoditiesService
{
    Task<IEnumerable<TradeActionHistoryDto>> GetTradeActionHistory();
    Task<IEnumerable<CommoditySummaryDto>> GetCommoditySummary();

    Task<IEnumerable<ModelDto>> GetModels();
    Task<IEnumerable<CommodityDto>> GetCommodities();
    
    Task<IEnumerable<ChartDto>> GetChartDetail(long commodityId, long modelId);
}