using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ExchangeView
{
    public class ExchangeRateResponse
    {
        public Meta Meta { get; set; }
        public Dictionary<string, CurrencyData> Data { get; set; }
    }

    public class Meta
    {
        public DateTime LastUpdatedAt { get; set; }
    }

    public class CurrencyData
    {
        public string Code { get; set; }
        public double Value { get; set; }
    }

}
