using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelBookingHistoryView
{
    public class ParcelBookingSummaryCountsView
    {

            public int PaidCount { get; set; }
            public int PendingCount { get; set; }
            public int DeliveryCount { get; set; }

            public int TotalParcelCount {  get; set; }
    }
}
