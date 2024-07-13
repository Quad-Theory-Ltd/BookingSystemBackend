using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.DiscountedOfferView
{
    public class DiscountedOfferView
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int CargoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DiscontDescription { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
