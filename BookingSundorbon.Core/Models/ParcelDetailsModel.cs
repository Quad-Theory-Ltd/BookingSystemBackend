using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Core.Models
{
    public class ParcelDetailsModel
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public int ProductTypeId { get; set; }
        public int NumberOfItem { get;set; }
        public int ParcelContentId { get; set; }
        public DateTime PickUpDate { get; set; }
        public bool HasPackaging { get; set; }
        public bool HasInsurance { get; set; }
        public bool IsFragileItem { get; set; }
        public bool IsInstantPickUp { get; set; }
        public float Weight { get; set; }
        public float Dimention { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
