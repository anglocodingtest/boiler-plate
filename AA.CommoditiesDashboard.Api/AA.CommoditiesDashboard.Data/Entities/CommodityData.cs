namespace AA.CommoditiesDashboard.Data.Entities;

public class CommodityData
{
    public long Id { get; set; }
    public DateTime Date { get; init; }
    public string Contract { get; init; }
    public decimal Price { get; init; }
    public int Position { get; init; }
    public int NewTradeAction { get; init; }
    public decimal PnlDaily { get; init; }
    public Model Model { get; init; }
    public Commodity Commodity { get; init; }
}