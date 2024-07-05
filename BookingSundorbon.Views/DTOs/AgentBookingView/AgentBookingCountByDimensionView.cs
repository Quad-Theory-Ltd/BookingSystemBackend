using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.AgentBookingView
{
    public class AgentBookingCountByDimensionView
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string DimensionName { get; set; }
        public int BookingCount { get; set; }
    }
}
