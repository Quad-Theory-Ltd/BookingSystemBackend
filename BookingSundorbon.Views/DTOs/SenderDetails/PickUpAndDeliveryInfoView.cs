using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.SenderDetails
{
    public class PickUpAndDeliveryInfoView
    {
        public int ParcelOrderId { get; set; }
        public string PickpupPointAddress { get; set; }
        public string DeliveryPointAddress { get; set; }
    }
}
