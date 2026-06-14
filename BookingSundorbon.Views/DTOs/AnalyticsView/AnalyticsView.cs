using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AnalyticsView
{
    public class AdminBookingAnalyticsSummary
    {
        /// <summary>Number of parcels (bookings), not line items.</summary>
        public int TotalBookings { get; set; }
        /// <summary>Parcel fully paid (all line items paid).</summary>
        public int PaidCount { get; set; }
        /// <summary>Parcel not fully paid.</summary>
        public int UnpaidCount { get; set; }
        public int PaidPercent { get; set; }
        public int UnpaidPercent { get; set; }
        /// <summary>Total product lines across all parcels.</summary>
        public int TotalLineItems { get; set; }
        /// <summary>Sum of parcel totals (same logic as admin bookings).</summary>
        public decimal GrandTotal { get; set; }
        public string PrimaryCurrencyCode { get; set; }   // e.g. "GBP"
        public string PrimaryCurrencySymbol { get; set; } // e.g. "£"
    }
    public class PaymentBreakdownItem
    {
        public string Label { get; set; }  // "Paid" | "Not paid"
        public int Count { get; set; }
    }
    public class ProductGroupAnalyticsItem
    {
        public int? ProductGroupId { get; set; }      // ItemCategory / product group id
        public string ProductGroupName { get; set; } // e.g. "Fruits"
        public int LineItemCount { get; set; }      // orders in that group
        public int SharePercent { get; set; }       // % of total line items
        public decimal TotalAmount { get; set; }   // sum subTotal for lines in group
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
    }
    public class CurrencyTotalItem
    {
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
