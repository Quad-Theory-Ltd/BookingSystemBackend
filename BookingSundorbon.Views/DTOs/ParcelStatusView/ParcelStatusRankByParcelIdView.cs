using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelStatusView
{
    public class ParcelStatusRankByParcelIdView
    {
    

        public int ParcelId { get; set; }
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public int ParcelStatusId { get; set; }
        public string ParcelStatusName { get; set; }
        public int Rank {  get; set; }

    }
}
