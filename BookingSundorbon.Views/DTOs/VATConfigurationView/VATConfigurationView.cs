using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.VATConfigurationView
{
    public class VATConfigurationView
    {
        public int Id { get; set; }
        public string ConfigurationDescription { get; set; }
        public decimal ConfigurationPercentage { get; set; }
        public decimal ConfigurationAmount { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
