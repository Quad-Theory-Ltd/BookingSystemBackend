using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.RouteView
{
    public class CreateRouteTypeView
    {
        public int CompanyId { get; set; }
        public string RouteName { get; set; }
        public string StartingArea { get; set; }
        public string EndingArea { get; set; }
        public decimal RouteCost { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
