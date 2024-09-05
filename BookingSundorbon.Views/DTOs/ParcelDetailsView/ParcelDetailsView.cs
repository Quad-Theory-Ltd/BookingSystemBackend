using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelDetailsView
{
    public class ParcelDetailsView
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ParcelParentId { get; set; }
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public decimal RouteCost { get; set; }
        public int ShippingServiceId { get; set; }
        public decimal ShippingServicePercentage { get; set; }
        public decimal ShippingServiceAmount { get; set; }
        public int DiscountedOfferId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsExtraPackaging { get; set; }
        public decimal ExtraPackagingCost { get; set; }
        public bool IsPickup { get; set; }
        public decimal PickUpCost { get; set; }
        public int ItemTypeId { get; set; }
        public decimal ItemTypeCost { get; set; }
        public int WeightId { get; set; }
        public decimal WeightCost { get; set; }
        public int DimensionId { get; set; }
        public string BoxName { get; set; }
        public decimal DimensionCost { get; set; }
        public int CargoTypeId { get; set; }
        public string CargoTypeName { get; set; }
        public decimal CargoCost { get; set; }
        public  string ParcelAdditionalInfo { get; set; }
        public string UniqItemDescription { get; set; }
        public int ItemValue { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }


    }
}
