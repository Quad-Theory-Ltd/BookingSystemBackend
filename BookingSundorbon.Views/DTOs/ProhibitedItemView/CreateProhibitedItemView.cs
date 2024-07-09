using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ProhibitedItemView
{
    public class CreateProhibitedItemView
    {
        public int CompanyId { get; set; }
        public string ItemName { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
