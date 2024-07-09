using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.PickupView
{
    public class CreatePickupView
    {
        public string PickUpDescription { get; set; }
        public decimal Cost { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
