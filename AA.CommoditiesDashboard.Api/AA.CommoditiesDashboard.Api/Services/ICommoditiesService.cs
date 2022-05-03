using AA.CommoditiesDashboard.Api.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AA.CommoditiesDashboard.Api.Services
{
    public interface ICommoditiesService
    {
        Task<IEnumerable<TradeAction>> GetTradeActions();
        Task<IEnumerable<ModelCommodity>> GetModelCommodities();
        Task<IEnumerable<ChartPoint>> GetChartData(long id);
    }
}
