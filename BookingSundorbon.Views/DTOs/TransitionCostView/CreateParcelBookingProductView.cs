using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.TransitionCostView
{
    public class CreateParcelBookingProductView
    {
        public int ItemCategoryId { get; set; }
        public decimal ItemCategoryCost { get; set; }

        public int ItemTypeId { get; set; }
        public decimal ItemTypeCost { get; set; }

        public int DimensionId { get; set; }
        public decimal DimensionCost { get; set; }

        public int WeightId { get; set; }
        public decimal WeightCost { get; set; }

        public string UniqItemDescription { get; set; }
        public decimal ItemValue { get; set; }

        public bool IsExtraPackaging { get; set; }
        public decimal ExtraPackagingCost { get; set; }

        public string ParcelAdditionalInfo { get; set; }

        public int CargoTypeId { get; set; }
        public decimal CargoCost { get; set; }

        public int ProductQty { get; set; }
    }
}
