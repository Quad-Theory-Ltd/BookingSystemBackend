using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Core.Models
{
    public class PriceSetupModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int RoutingTypeId { get; set; }
        public int ParcelLength { get; set; }
        public int ParcelWidth { get; set; }
        public int ParcelHeight { get; set; }
        public decimal ParcelWeight { get; set; }
        public int AdditionalPriceId { get; set; }
        public string CargoType { get; set; }
        public decimal Price { get; set; }
        public int ItemTypeId { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
