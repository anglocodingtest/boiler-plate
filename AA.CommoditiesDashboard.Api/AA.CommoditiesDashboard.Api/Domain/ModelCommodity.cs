namespace AA.CommoditiesDashboard.Api.Domain
{
    public class ModelCommodity
    {
        public long Id { get; set; }
        public string ModelName { get; set; }
        public string CommodityName{ get; set; }
        public int Position { get; set; }
        public decimal VarAllocation { get; set; }
        public decimal PnLDaily { get; set; }
        public decimal Price { get; set; }
        public decimal PnlLTD { get; set; }
    }
}
