using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelBookingInformationView
{
    public class ParcelHeaderDto
    {
        public int ParcelNo { get; set; }

        public int ParcelStatusId { get; set; }
        public string ParcelStatusName { get; set; }

        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string SenderPostCode { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverPostCode { get; set; }

        public string StripePaymentIntentId { get; set; }
        public string StripePaymentMethod { get; set; }
        public string CardBrand { get; set; }
        public string CardLast4 { get; set; }
        public string StripeChargeId { get; set; }
    }

    public class ParcelDetailDto
    {
        public int ParcelNo { get; set; }

        public DateTime ParcelCreationDate { get; set; }

        public string RouteName { get; set; }
        public string CargoTypeName { get; set; }
        public string ShippingServiceName { get; set; }

        public string WeightDescription { get; set; }
        public string DimensionName { get; set; }

        public decimal DimensionCost { get; set; }
        public string ParcelAdditionalInfo { get; set; }
        public string UniqItemDescription { get; set; }

        public string AgentName { get; set; }
        public decimal FixedCommisionAmount { get; set; }
        public decimal AgentCommisionPayable { get; set; }

        public decimal SubTotal { get; set; }

        public string PaymentStatus { get; set; }

        public string CurrencyType { get; set; }
        public string Symbol { get; set; }
    }
    public class ParcelResponseDto
    {
        public int ParcelNo { get; set; }

        public int ParcelStatusId { get; set; }
        public string ParcelStatusName { get; set; }

        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhone { get; set; }
        public string SenderPostCode { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverPostCode { get; set; }

        public string StripePaymentIntentId { get; set; }
        public string StripePaymentMethod { get; set; }
        public string CardBrand { get; set; }
        public string CardLast4 { get; set; }
        public string StripeChargeId { get; set; }

        public List<ParcelDetailDto> Details { get; set; } = new();
    }
}
