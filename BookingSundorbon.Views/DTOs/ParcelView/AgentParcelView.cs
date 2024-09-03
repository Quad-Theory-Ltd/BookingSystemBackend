using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelView
{
    public class AgentParcelView
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int ParcelNo { get; set; }
        public string RecordSerialNo { get; set; }
        public string recordSerialNoWithParcelNo { get; set; }
        public string Barcode { get; set; }
        public string RouteName { get; set; }
        public string ReceiverName { get; set; }
        public string ReciverAddress { get; set; }
    }
}
