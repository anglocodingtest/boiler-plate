namespace AA.CommoditiesDashboard.Service.Dtos;

public class CommoditySummaryDto
{
    public long ModelId { get; init; }
    public long CommodityId { get; init; }
    public string ModelName { get; init; }
    public string CommodityName { get; init; }
    public DateTime Date { get; init; }
    public int Position { get; init; }
    public decimal PnlCurrent { get; init; }
    public decimal Price { get; init; }
    public decimal PnlLtd { get; init; }

    public IEnumerable<YearSummaryDto> YearSummaries { get; set; }
}

public class YearSummaryDto
{
    public int Year { get; init; }
    public decimal PnlYtd { get; init; }
    public decimal DrawdownYtd { get; init; }
}