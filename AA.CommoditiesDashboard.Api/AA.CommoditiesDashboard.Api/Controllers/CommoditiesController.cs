using AA.CommoditiesDashboard.Service;
using AA.CommoditiesDashboard.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AA.CommoditiesDashboard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommoditiesController : ControllerBase
{
    private readonly ICommoditiesService _commoditiesService;
    public CommoditiesController(ICommoditiesService commoditiesService)
    {
        _commoditiesService = commoditiesService;
    }

    [HttpGet()]
    public async Task<IEnumerable<CommodityDto>> Get()
    {
        return await _commoditiesService.GetCommodities();
    }

    [HttpGet("models")]
    public async Task<IEnumerable<ModelDto>> GetModels()
    {
        return await _commoditiesService.GetModels();
    }

    [HttpGet("summary")]
    public async Task<IEnumerable<CommoditySummaryDto>> GetCommoditySummary()
    {
        return await _commoditiesService.GetCommoditySummary();
    }

    [HttpGet("tradeactions")]
    public async Task<IEnumerable<TradeActionHistoryDto>> GetTradeActions()
    {
        return await _commoditiesService.GetTradeActionHistory();
    }

    [HttpGet("chartdetail/{commodityId}/{modelId}")]
    public async Task<IEnumerable<ChartDto>> GetChartDetail(long commodityId, long modelId)
    {
        return await _commoditiesService.GetChartDetail(commodityId, modelId);
    }
}

