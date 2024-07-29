using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Core.Models
{
    public class ReceiverDetailsModel
    {
        public int Id { get; set; }
        public int ParcelOrderId { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NearestLandmark { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public bool IsActive { get; set; }
        public string? CreatorId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? ModifierId { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
