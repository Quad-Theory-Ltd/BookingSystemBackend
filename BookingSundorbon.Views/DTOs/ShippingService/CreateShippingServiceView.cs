using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ShippingService
{
    public class CreateShippingServiceView
    {
        public int RouteId { get; set; }
        public int CargoId { get; set; }
        public bool IsExpressService { get; set; }
        public string ServiceName { get; set; }
        public int Days { get; set; }
        public decimal ShippingServiceAmount { get; set; }
        public decimal ShippingServiceAmountPercentage { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
