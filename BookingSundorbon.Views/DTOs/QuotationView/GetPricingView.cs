namespace BookingSundorbon.Views.DTOs.QuotationView
{
    public class GetPricingView
    {
        public int RouteId { get; set; }
        public int MeasurementUnitId { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string QuotationType { get; set; }
    }
}

