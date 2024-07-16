using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.BarcodeStatusView
{
    public class BarcodeStatusView
    {
        public int Id { get; set; }
        public string BarcodeNumber { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierId { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
