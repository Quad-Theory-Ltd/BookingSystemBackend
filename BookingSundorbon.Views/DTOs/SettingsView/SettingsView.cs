using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.SettingsView
{
    public class SettingsView
    {
        public int Id { get; set; }
        public string GtmHeadCode { get; set; }
        public string GtmBodyCode { get; set; }
        public bool IsActive { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class GtmSettingsView
    {
        public string GtmHeadCode { get; set; }
        public string GtmBodyCode { get; set; }
    }

    public class SaveGtmSettingsView
    {
        public string GtmHeadCode { get; set; }
        public string GtmBodyCode { get; set; }
    }
}
