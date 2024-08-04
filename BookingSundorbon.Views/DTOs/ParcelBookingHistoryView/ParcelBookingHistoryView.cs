using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelBookingHistoryView
{
    public class ParcelBookingHistoryView
    {
        public int ParcelNo { get; set; }
        public DateTime ParcelCreationDate { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string RouteName { get; set; }
        public string CargoTypeName { get; set; }
        public string ShippingServiceName { get; set; }
        public string WeightDescription { get; set; }
        public string DimensionName { get; set; }
        public decimal DimensionCost { get; set; }
        public string ParcelAdditionalInfo { get; set; }
        public string UniqItemDescription { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }

        public decimal? CommisionPercentage { get; set; }
        public decimal? FixedCommisionAmount { get; set; }
        public decimal? AgentCommisionPayable { get; set; }
        public decimal SubTotal { get; set; }
    }
}
