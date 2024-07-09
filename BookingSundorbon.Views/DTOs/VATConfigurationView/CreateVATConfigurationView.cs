using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.VATConfigurationView
{
    public class CreateVATConfigurationView
    {
        public string ConfigurationDescription { get; set; }
        public decimal ConfigurationPercentage { get; set; }
        public decimal ConfigurationAmount { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
    }
}
