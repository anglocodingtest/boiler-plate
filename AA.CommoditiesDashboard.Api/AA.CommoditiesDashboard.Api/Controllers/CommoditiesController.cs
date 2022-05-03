using AA.CommoditiesDashboard.Api.Domain;
using AA.CommoditiesDashboard.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommoditiesController : ControllerBase
    {
        private readonly ICommoditiesService _commoditiesService;

        public CommoditiesController(ICommoditiesService commoditiesService)
        {
            _commoditiesService = commoditiesService ?? throw new ArgumentNullException(nameof(commoditiesService));
        }

        [HttpGet]
        public async Task<IEnumerable<ModelCommodity>> Get()
            => await _commoditiesService.GetModelCommodities();

        [HttpGet]
        [Route("tradeactions")]

        public async Task<IEnumerable<TradeAction>> GetTradeActions()
            => await _commoditiesService.GetTradeActions();

        [HttpGet]
        [Route("chartdata/{id}")]
        public async Task<IEnumerable<ChartPoint>> GetChartData(long id)
            => await _commoditiesService.GetChartData(id);
    }
}
