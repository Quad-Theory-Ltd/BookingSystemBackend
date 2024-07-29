using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.GetTransitionCostView
{
    public class GetTransitionCostView
    {
        public int RoutingTypeId { get; set; }
        public decimal ParcelLength { get; set; }
        public decimal ParcelWidth { get; set; }
        public decimal ParcelHeight { get; set; }
        public decimal ParcelWeight { get; set; }
        public int CargoTypeId { get; set; }
        public bool IsExtraPackaging { get; set; }
        public bool IsPickUp { get; set; }
        public int ItemTypeId { get; set; }
        public DateTime ShipmentArrivalDate { get; set; }

    }
}

      