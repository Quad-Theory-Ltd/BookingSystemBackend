using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.DiscountedOfferDetailView
{
    public class DiscountedOfferDetailView
    {
        public int Id { get; set; }
        public int DiscuntedOfferID { get; set; }
        public DateTime DiscountedDate { get; set; }
        public decimal DiscountedPercentage { get; set; }
        public decimal DiscountedAmount { get; set; }
        public int BranchId { get; set; }
    }
}
