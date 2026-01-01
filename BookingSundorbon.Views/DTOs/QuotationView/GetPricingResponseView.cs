namespace BookingSundorbon.Views.DTOs.QuotationView
{
    public class GetPricingResponseView
    {
        public int IsSuccess { get; set; }
        public string Message { get; set; }
        public int? DimensionId { get; set; }
        public string DimensionName { get; set; }
        public int? RouteId { get; set; }
        public decimal? MinimumWeight { get; set; }
        public decimal? MaximumWeight { get; set; }
        public decimal? DimensionVolume { get; set; }
        public string UnitDescription { get; set; }
        public decimal? Cost { get; set; }
        public string QuotationType { get; set; }
    }
}

