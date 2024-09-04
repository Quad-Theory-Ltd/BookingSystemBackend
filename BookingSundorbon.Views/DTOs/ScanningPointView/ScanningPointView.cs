using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ScanningPointView
{
    public class ScanningPointView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ScanningPointName { get; set; }
        public int ParcelStatusId { get; set; }
        public string  ParcelStatusName { get; set; }
        public int DeviceId { get; set; }
        public bool IsAgent { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int SubBranchId { get; set; }
        public string SubBranchName { get; set; }
        public string UserName { get; set; }

    }
}
