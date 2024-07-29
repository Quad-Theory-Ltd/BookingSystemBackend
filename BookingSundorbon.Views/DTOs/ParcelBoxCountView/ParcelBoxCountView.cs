using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Views.DTOs.ParcelBoxCountView
{
    public class ParcelBoxCountView
    {
        public string BoxName { get; set; }
        public int UserBoxParcelCount { get; set; }
        public int AgentBoxParcelCount { get; set; }
        public int TotalBoxParcelCount { get; set; }
    }
}
