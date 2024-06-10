using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Core.Models
{
    public class CompanyModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyWebsite { get; set; }
        public string BIN { get; set; }
        public string TIN { get; set; }
        public string VATRegNo { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }

    }
}
