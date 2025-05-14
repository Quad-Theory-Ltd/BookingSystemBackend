using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.TransitionCostView
{
    public class CreateParcelBookingOutputView
    {
        public int ParcelId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string  Message { get; set; }
        public string Barcode { get; set; }

        //public int RouteId { get; set; }
    }
}
