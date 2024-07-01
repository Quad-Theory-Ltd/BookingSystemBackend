using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.GetTransitionCostView
{
    public class GetTransitionCostView
    {
        public decimal ParcelLength { get; set; }
        public decimal ParcelWidth { get; set; }
        public decimal ParcelHeight { get; set; }
        public decimal ParcelWeight { get; set; }
        public string CargoType { get; set; }
        public bool IsExtraPackaging { get; set; }
        public bool IsFragileItem { get; set; }
        //public int RoutingTypeId { get; set; }
    }
}
