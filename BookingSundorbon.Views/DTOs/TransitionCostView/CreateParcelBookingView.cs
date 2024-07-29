using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.TransitionCostView
{
    public class CreateParcelBookingView
    {
        public int CompanyId { get; set; }
        public int RoutingTypeId { get; set; }
        public decimal RouteCost { get; set; }

        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderMobileNo  { get; set; }
        public int SenderCountryId { get; set; }
        public int SenderCityId { get; set; }
        public string SenderLandMark { get; set; }
        public string SenderAdditionalAddressInfo { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverMobileNo { get; set; }
        public int ReceiverCountryId { get; set; }
        public int ReceiverCityId { get; set; }
        public string ReceiverLandMark { get; set; }
        public string ReceiverAdditionalAddressInfo { get; set; }

        public int ItemCategoryId { get; set; }
        public int ItemTypeId { get; set; }
        public decimal ItemTypeCost { get; set; }

      
        public int DimensionId { get; set; }
        public decimal DimensionCost { get; set; }
        public int WeightId { get; set; }
        public decimal WeightCost { get; set; }

        public string UniqItemDescription { get; set; }
        public decimal ItemValue { get; set; }
        public  bool IsExtraPackaging { get; set; }
        public  decimal ExtraPackagingCost { get; set; }
        public string ParcelAdditionalInfo { get; set; }

        public bool  IsPickup { get; set; }
        public DateTime? PickupDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int PickUpCountryId { get; set; }
        public int PickUpCityId { get; set; }
        public string PickUpLandMark { get; set; }
        public string PickUpAdditionalAddressInfo { get; set; }
        public string ParcelPickUpInstractions { get; set; }
        public decimal PickupCost { get; set; }

        public int CargoTypeId { get; set; }
        public decimal CargoCost { get; set; }

        public decimal SubTotal { get; set; }
        public decimal VAT_TaxParcentage { get; set; }
        public decimal VAT_TaxAmount { get; set; }
        public decimal OrderPayableAmount { get; set;}

        public int ShippingServiceId { get; set; }
        public decimal ShippingServicePercentage { get; set; }
        public decimal ShippingServiceAmount { get; set; }

        public decimal DiscountPercentage { get;set; }
        public decimal DiscountedOfferId { get;set; }
        public decimal DiscountAmount { get;set; }
        public string? CreatorId { get; set; }
        public string ModifierId { get; set; }
        public bool? IsActive { get; set; }
        public string? Barcode { get; set; }
        public bool IsAgent { get; set; }
        public string? AgentId { get; set; }


    }
}
