namespace AA.CommoditiesDashboard.Service.Dtos;

public class TradeActionHistoryDto
{   public string ModelName { get; init; }
    public string CommodityName { get; init; }
    public int TradeAction { get; init; }
    public DateTime Date { get; init; }
}