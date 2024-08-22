using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.BranchView
{
    public class BranchView
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public int SubBranchId { get; set; }
        public string SubBranchName { get; set; }
        public string AddressLine { get; set; }
        public bool IsActive { get; set; }
        public decimal BranchDiscountPercentage { get; set; }
        public decimal BranchDiscountAmount { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
