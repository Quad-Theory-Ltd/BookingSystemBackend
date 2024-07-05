using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelBookingInformationView
{
    public class ParcelInfoByUserIdView
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int CompanyId { get; set; }
        public int ParcelParentId { get; set; }
        public int RouteId { get; set; }
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
        public decimal PickupCost { get; set; }
        public int ItemTypeId { get; set; }
        public decimal ItemTypeCost { get; set; }
        public int WeightId { get; set; }
        public decimal WeightCost { get; set; }
        public int DimensionId { get; set; }
        public decimal DimensionCost { get; set; }
        public int CargoTypeId { get; set; }
        public decimal CargoCost { get; set; }
        public string ParcelAdditionalInfo { get; set; }
        public string UniqItemDescription { get; set; }
        public decimal ItemValue { get; set; }
        public string StartingArea { get; set; }
        public string EndingArea { get; set; }
        public string RouteName { get; set; }
        public string DimensionName { get; set; }
        public string WeightDescription { get; set; }
        public string TotalCost { get; set; }
        
    }
}
