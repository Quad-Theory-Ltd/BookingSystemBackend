using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Core.Models
{
    public class PickupDetails
    {
        public int Id { get; set; }
        public bool IsPickup  { get; set; }
        public DateOnly PickupDate { get; set; }
        public TimeOnly PickTimeFrom { get; set; }
        public TimeOnly PickupTimeTo { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string PickupAdditionalAddressInfo { get; set; }
        public string Instructions { get; set;}       
        public DateTime? CreationDate { get; set; }
        public string? CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public string ModifierId { get; set; }
    

    }

}
