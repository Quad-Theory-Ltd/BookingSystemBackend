using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ShippingServiceView
{
    public class ShippingServiceView
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int CargoId { get; set; }
        public bool IsExpressService { get; set; }
        public string ServiceName { get; set; }
        public int Days { get; set; }
        public decimal ShippingServiceAmount { get; set; }
        public decimal ShippingServiceAmountPercentage { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
