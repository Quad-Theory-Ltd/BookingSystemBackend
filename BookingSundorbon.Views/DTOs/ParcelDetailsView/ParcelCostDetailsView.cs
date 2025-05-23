using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelDetailsView
{
    public class ParcelCostDetailsView
    {

            public int ParcelId { get; set; }
            public decimal SubTotal { get; set; }
            public decimal VAT_TaxAmount { get; set; }

            public decimal RouteCost { get; set; }
            public decimal ShippingServicePercentage { get; set; }
            public decimal ShippingServiceAmount { get; set; }

            public decimal DiscountPercentage { get; set; }
            public decimal DiscountAmount { get; set; }

            public decimal ExtraPackagingCost { get; set; }
            public decimal PickUpCost { get; set; }

            public decimal ItemTypeCost { get; set; }
            public decimal WeightCost { get; set; }
            public decimal DimensionCost { get; set; }
            public decimal CargoCost { get; set; }


    }
}
