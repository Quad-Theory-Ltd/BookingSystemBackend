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
        public int CreationDate { get; set; }
        public int CompanyId { get; set; }
        public int ParcelParentId { get; set; }
        public int RouteId { get; set; }
        public decimal RouteCost { get; set; }
        public int ShippingServiceId { get; set; }
        public decimal ShippingServicePercentage { get; set; }
        public decimal ShippingServiceAmount { get; set; }
        public int DiscountOfferId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsExtraPackaging { get; set; }
        public decimal ExtraPackagingCost { get; set; }
        public bool IsPickup { get; set; }
        public decimal PickupCost { get; set; }
        public int ItemTypeId { get; set; }
        public decimal ItemTypeCost { get; set; }
        public int WeightId { get; set; }
        public int MyProperty { get; set; }
    }
}
