using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.RoleView
{
    public class RoleView
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDefault { get; set; }
    }
}
