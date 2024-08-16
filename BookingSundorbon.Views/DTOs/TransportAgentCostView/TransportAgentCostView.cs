using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.TransportAgentCostView
{
    public class TransportAgentCostView
    {
        public int Id { get; set; }
        public int parcelNo { get; set; }
        public string PickUpPoint { get; set; }
        public string DeliveryPoint { get; set; }
        public int TransportAgentId { get; set; }
        public decimal Cost { get; set; }

    }
}
