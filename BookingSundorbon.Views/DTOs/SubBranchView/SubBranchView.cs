using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.SubBranchView
{
    public class SubBranchView
    {
        public int Id { get; set; }
        public string SubBranchName { get; set; }
        public bool IsHub { get; set; }
        public bool IsOffice { get; set; }
        public bool IsAgent { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int EmployeId { get; set; }
        public string EmployeName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; } 
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }


    }
}
